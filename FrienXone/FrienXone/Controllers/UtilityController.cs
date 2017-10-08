using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FrienXone.Services;
using FrienXone.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FrienXone.Controllers
{
    public class UtilityController : Controller
    {
        private readonly UtilityService utilityService;
        private readonly LuisService luisService;
        private readonly DatabaseService databaseService;

        public UtilityController()
        {
            this.utilityService = new UtilityService();
            this.luisService = new LuisService();
            this.databaseService = new DatabaseService();
        }

        // POST api/v1/User/FaceAttributes
        [HttpPost]
        [Route("api/v1/User/FaceAttributes")]
        public async Task<string> MakeAnalysisRequest([FromBody]string url)
        {
            return await utilityService.MakeAnalysisRequest(url);
        }

        [HttpPost]
        [Route("api/v1/query/{query}")]
        public async Task<IEnumerable<ApplicationUser>> Query([FromBody]ApplicationUser user, string query)
        {
            var processedQuery = await this.luisService.ProcessQuery(query);

            if(processedQuery.Intent.Equals("FindFriend"))
            {
                string hobby = string.Empty;
                string language = string.Empty;
                string accessories = string.Empty;
                if (processedQuery.Type == "hobby")
                {
                    hobby = processedQuery.Entity;
                }

                if(processedQuery.Type == "language")
                {
                    language = processedQuery.Entity;
                }

                if(processedQuery.Type == "accessories")
                {
                    accessories = processedQuery.Entity;
                }

                return await this.databaseService.QueryUser(hobby:hobby, language: language, accessories: accessories);
            }

            return null;
        }

        // POST api/v1/User/query/{language}/{accessories}/{hobby}
        [HttpPost]
        [Route("api/v1/query/{language}/{hobby}")]
        public async Task<IEnumerable<ApplicationUser>> FindMatch(string language, string accessories, string hobby)
        {
            return await this.databaseService.QueryUser(hobby: hobby, language: language, accessories: accessories);
        }
    }
}
