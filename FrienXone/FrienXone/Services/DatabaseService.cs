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
            List<ApplicationUser> Users = new List<ApplicationUser>();
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = 20 };

            var query = this.client.CreateDocumentQuery<ApplicationUser>(UriFactory.CreateDocumentCollectionUri(DatabaseName, UsersCollection), queryOptions)
                                                .AsEnumerable();

            foreach (var item in query)
            {
                if(!string.IsNullOrEmpty(hobby))
                {
                    if(item.Hobbies.Select(i => i.ToLower()).Contains(hobby.ToLower()))
                    Users.Add(item);
                }

                if (!string.IsNullOrEmpty(gender))
                {
                    if (item.Gender.ToLower() != gender.ToLower())
                        Users.Add(item);
                }

                if (!string.IsNullOrEmpty(language))
                {
                    if (language.ToLower() == item.Language.ToLower())
                        Users.Add(item);
                }
            }

            return Users.Distinct().OrderBy(i => i.Location);
        }
    }
}
