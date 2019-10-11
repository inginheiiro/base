using System.Collections.Generic;
using System.Threading.Tasks;
using Base.DataAccess;
using Base.Models;
using Base.Models.Base;
using Base.Settings.Email.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace Base.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
                                
        
        [HttpGet]
        [AllowAnonymous]
        [Route("Bootstrap")]
        public async Task<ObjectResult> Bootstrap()
        {
            var uda= new UserDataAccess();
            await uda.Bootstrap();
            return  (Ok(new {bootstrap = true}));
        }

        [Route("Get")]
        public async Task<UsersModel> GetUser(string email)
        {
            var uda= new UserDataAccess();
            return await uda.FindUserByEmail(email);            
        }
                        
        
    }
}