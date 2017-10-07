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
        public async Task Query([FromBody]ApplicationUser user, string query)
        {
            var processedQuery = this.luisService.ProcessQuery(query);
        }
    }
}
