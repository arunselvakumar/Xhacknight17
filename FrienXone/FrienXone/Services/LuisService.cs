using FrienXone.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrienXone.Services
{
    public class LuisService
    {
        private const string LuisEndPoint = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/74d57c96-ca5b-4cc9-a013-916a9f6a7573?subscription-key=16f65b8517c74aa9b84328ffc1c78140&verbose=true&timezoneOffset=0&q=";

        public async Task<QueryResponse> ProcessQuery(string query)
        {
            var restClient = new RestClient();
            var request = new RestRequest();
            request.Method = Method.GET;

            restClient.BaseUrl = new Uri(LuisEndPoint + query);
            var response = restClient.Execute<LuisResponse>(request);

            return new QueryResponse { Intent = response.Data.TopScoringIntent.Intent, Entity = response.Data.Entities.FirstOrDefault().Entity, Type = response.Data.Entities.FirstOrDefault().Type };
        }
    }
}
