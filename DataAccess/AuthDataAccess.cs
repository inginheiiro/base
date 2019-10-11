
using System;
using System.Security;
using Base.Models.Base;
using Google.Apis.Auth;
using MongoDB.Entities;
using Serilog;

namespace Base.DataAccess
{
    public class AuthDataAccess
    {

        public UsersModel SignIn(string email, string password)
        {
            var valid = false;

            UsersModel u = new UsersModel();
            var users = new UserDataAccess();

            if (!string.IsNullOrWhiteSpace(email))
            {
                try
                {
                    var baseUser = users.FindUserByEmail(email).Result;
                    valid = (baseUser != null &&
                             email == baseUser.Email &&
                             Helpers.Utils.sha256_hash(password) == baseUser.AccessKey &&
                             baseUser.Enabled && !baseUser.Deleted);
                    u = baseUser;
                    if (u != null) u.AccessKey = null;
                }
                catch (Exception e)
                {
                    Log.Error(e.Message);
                    throw;
                }
            }

            if (!valid)
            {
                throw new SecurityException("Forbidden");
            }

            return u;
        }


        public UsersModel SignInGoogle(string idToken)
        {
            UsersModel u = new UsersModel();
            try
            {
                var validPayload = GoogleJsonWebSignature.ValidateAsync(idToken).Result;                
                var users = new UserDataAccess();
                var baseUser = users.FindUserByEmail(validPayload.Email).Result;

                if (baseUser != null)
                {                    
                    u = baseUser;                    
                }
                else
                {
                    u.AccountType = "google";
                    u.PhoneNumber = null;
                    u.Fullname = validPayload.Name;
                    u.Email = validPayload.Email;
                    u.Enabled = false;
                    u.Deleted = false;
                    u.Photo = validPayload.Picture;
                    u.AccessKey = null;
                    u.Save();
                    u.Roles.Add(users.GetAllRoles("user").Result[0]);                          
                }
            }
            catch (InvalidJwtException e)
            {
                Log.Error(e.Message);
                throw new SecurityException("Forbidden");
            }

            return u;
        }


    }
}