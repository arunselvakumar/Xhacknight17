using FrienXone.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FrienXone.Services
{
    public class DatabaseService
    {
        private const string EndpointUri = "https://frienxone.documents.azure.com:443/";
        private const string PrimaryKey = "GI4ocyftMyfCGsY7KyBAv3H8W3lz91c74lM9IpwEQnMaeUnerrRpGvfAvXdjfdi7pEkHyIHyUaNHxSpGWKECRw==";
        private const string UsersCollection = "Users";

        private const string DatabaseName = "UsersDatabaseId";

        private DocumentClient client;

        public DatabaseService()
        {
            this.client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);
        }

        public async Task<bool> CreateUser(ApplicationUser user)
        {
            await this.client.CreateDatabaseIfNotExistsAsync(new Database { Id = DatabaseName });
            await this.client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DatabaseName), new DocumentCollection { Id = UsersCollection });

            try
            {
                await this.client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseName, UsersCollection, user.UserName));
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseName, UsersCollection), user);
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public async Task<bool> Login(ApplicationUser user)
        {
            await this.client.CreateDatabaseIfNotExistsAsync(new Database { Id = DatabaseName });
            await this.client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DatabaseName), new DocumentCollection { Id = UsersCollection });

            FeedOptions queryOptions = new FeedOptions { MaxItemCount = 10 };

            var query = this.client.CreateDocumentQuery<ApplicationUser>(UriFactory.CreateDocumentCollectionUri(DatabaseName, UsersCollection), queryOptions)
                                                .Where(f => f.UserName == user.UserName);

            if(query == null)
            {
                return false;
            }

            foreach (var item in query)
            {
                if(item.Password == user.Password)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<IEnumerable<ApplicationUser>> QueryUser(string hobby = null, string gender = null, string accessories = null, string location = null, string age = null, string language = null)
        {
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = 20 };

            var query = this.client.CreateDocumentQuery<ApplicationUser>(UriFactory.CreateDocumentCollectionUri(DatabaseName, UsersCollection), queryOptions)
                                                .Where(user => user.Hobbies.Contains(hobby))
                                                .AsEnumerable();

            if(!string.IsNullOrEmpty(gender))
            {
                query = query.Where(user => user.Gender != gender);
            }

            if(accessories.Any())
            {
                query.Where(user => user.Accessories.Contains(accessories));
            }

            if(!string.IsNullOrEmpty(language))
            {
                query.Where(user => user.Language == language);
            }

            return query.OrderBy(user => user.Location);
        }
    }
}
