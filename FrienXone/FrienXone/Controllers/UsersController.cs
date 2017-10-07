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
    public class UsersController : Controller
    {
        private readonly DatabaseService databaseService;

        public UsersController()
        {
            this.databaseService = new DatabaseService();
        }

        [HttpPost]
        [Route("api/v1/User/Register")]
        public async Task<bool> CreateUser([FromBody]ApplicationUser user)
        {
            return await databaseService.CreateUser(user);
        }

        [HttpPost]
        [Route("api/v1/User/Login")]
        public async Task<bool> LoginUser(ApplicationUser user)
        {
            return await databaseService.Login(user);
        }
    }
}
