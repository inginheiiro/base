using Base.DataAccess;
using Base.Models.Base;
using JWTSimpleServer;
using JWTSimpleServer.Abstractions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
namespace Base.Custom
{
    public class JwtAuthenticationProvider: IAuthenticationProvider
    {         
        public Task ValidateClientAuthentication(JwtSimpleServerContext context)
        {            
            UsersModel u= null;
            try{
                var a = new AuthDataAccess();                
                if (context.UserName.Trim().ToLower() == "google")                
                    u= a.SignInGoogle(context.Password);
                else 
                    u= a.SignIn(context.UserName,context.Password);                                    

                if (u!=null && u.Enabled){
                
                    var claims = new List<Claim>
                    {
                        new Claim("email", u.Email),
                        new Claim("name", u.Fullname),
                        new Claim("social", u.AccountType),
                        new Claim("photo", u.Photo),
                        new Claim("enabled", JsonConvert.SerializeObject(u.Enabled)),
                        new Claim("roles", JsonConvert.SerializeObject(u.Roles.ChildrenQueryable().Select(p => p.Role).ToList()))
                    };

                    context.Success(claims);
                }

            } catch
            {               
             context.Reject("Invalid user authentication");
            }

             if (u!=null && u.Deleted)
                context.Reject("User is deleted");
             else if (u!=null && !u.Enabled)
                context.Reject("User is disabled");
             
                                     
            return Task.CompletedTask;            
        }
    }
}