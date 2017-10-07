using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FrienXone.Models;
using FrienXone.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FrienXone.Controllers
{
    public class UtilityController : Controller
    {
        private readonly LuisService luisService;

        public UtilityController()
        {
            this.luisService = new LuisService();
            this.databaseService = new DatabaseService();
        }

        [HttpPost]
        [Route("api/v1/query/{query}")]
        public async Task Query([FromBody]ApplicationUser user, string query)
        {
            var processedQuery = this.luisService.ProcessQuery(query);
        }
    }
}
