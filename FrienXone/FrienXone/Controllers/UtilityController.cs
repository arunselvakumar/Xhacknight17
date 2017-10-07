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

        public UtilityController()
        {
            this.utilityService = new UtilityService();
        }

        // POST api/v1/User/FaceAttributes
        [HttpPost]
        [Route("api/v1/User/FaceAttributes")]
        public async Task<string> MakeAnalysisRequest([FromBody]string value)
        {
            return await utilityService.MakeAnalysisRequest(value);
        }
    }
}
