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
            try
            {
                await this.client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseName, UsersCollection, user.UserName));
            }
            catch (DocumentClientException de)
            {
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseName, UsersCollection), user.UserName);
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
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            var query = this.client.CreateDocumentQuery<ApplicationUser>(UriFactory.CreateDocumentCollectionUri(DatabaseName, UsersCollection), queryOptions)
                                                .FirstOrDefault<ApplicationUser>(f => f.UserName == user.UserName);

            if(query == null)
            {
                return false;
            }

            if(query.UserName == user.UserName && query.Password == user.Password)
            {
                return true;
            }

            return false;
        }
    }
}
