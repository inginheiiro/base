using System;
using MongoDB.Driver.Linq;
using System.Linq;
using System.Threading.Tasks;
using Base.Helpers;
using Base.Models.Base;
using Base.Settings.Email.Interfaces;
using MimeKit;
using MongoDB.Entities;
using Serilog;
using System.Collections.Generic;

namespace Base.DataAccess
{
    public class UserDataAccess
    {                
                        
        /// <summary>
        /// Bootstrap
        /// </summary>
        public async Task Bootstrap()
        {
            try
            {                                
                var adminRole = new RolesModel() {Role = "admin", Default = false};

                var adminRoles = new[] {
                    adminRole,
                    new RolesModel() {Role = "user", Default = true},
                    new RolesModel() {Role = "manager", Default = false},
                    new RolesModel() {Role = "teacher", Default = false}
                };

                await  adminRoles.SaveAsync();
                
                var ux = new UsersModel()
                {
                    Email = "admin@admin.com",
                    Fullname = "Admin",
                    AccessKey = Helpers.Utils.sha256_hash("password"),
                    Enabled = true,
                    Deleted = false,
                    Photo =
                        "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAMDAwMDAwQEBAQFBQUFBQcHBgYHBwsICQgJCAsRCwwLCwwLEQ8SDw4PEg8bFRMTFRsfGhkaHyYiIiYwLTA+PlQBAwMDAwMDBAQEBAUFBQUFBwcGBgcHCwgJCAkICxELDAsLDAsRDxIPDg8SDxsVExMVGx8aGRofJiIiJjAtMD4+VP/CABEIAGAAYAMBIgACEQEDEQH/xAAbAAEBAQADAQEAAAAAAAAAAAAACAcEBQYJA//aAAgBAQAAAAD6QAADwOO8XevYAYbJIuDSARTloq7egSXhYp6igRLmI1a0gTDOopSlAfj87eveqv4BFOWtcswHV4jLIpzdO4eHwfE+GDlbRvkIgA//xAAXAQEBAQEAAAAAAAAAAAAAAAAAAwIB/9oACAECEAAAAKtMm6o5bqlh26HHboctoZ//xAAXAQADAQAAAAAAAAAAAAAAAAAAAQMC/9oACAEDEAAAAMCGGYl2ZiW0KDsxRLOWAN//xAA2EAABAwIDBQQIBgMAAAAAAAABAgMEBREGByEAEiAxURATQYEIFCIjMGFxkSQmMkBCgmJyof/aAAgBAQABPwD9hiTMvCWGVrakzO/kp0MaOnvFg9DyCdqj6QNQUtQptHYQnwVIWpwnyTu22gekBXEOj16lQnW/EMlbSvuor2wljihYyiKdgu7rqdHYztg4g/S+o+Y+BnJjuTh+KzR6c6pqXMbK3XUmym2b29noVEc9iSo3PPtBKSCDYjkRtlXiGRiPB8Z6SvvJEVa4zqzqVluxST87KF+POZxxeYFRC+TbUdKPp3aTw5AH8t1QdKhf7tp48+aYiPiWFOQpP4uFZQ8d5o2v9lcPo+SUKgVuNdO8l9le742UCL/8484XZa8f1Jt51SktJZSyDyQgtpVYeZ4cl5MhjHsNtsqCH2JCHR1SEFYv5p487MGVV+rorsGI4/HVEAlFA3y2Wr+0oD+NuAAqNhz2yYwJWabVnK1VIbkVAjFEdDuiypwi6t3mLAcciO1KjvMOp3m3UKQtPVKxY7VenuUmqToDmq4kl1lR6ltRTftwJDM/GNCZ3N4GoMqUn/FtQUr4Ob1Tw/UsVvKpbLiXmVFma5oEPONm28mx8ie3KLEeHMPVtZqTDpky1NMR5ACShgKNlFVyLX6jjqtbpFDj+s1KYxFa8FOrAv8AIDmT9NsV550hMCTGoLT7slbZS3JWju0IvpvAE3JHhe2xJUVEquTqSeDB+eFMap0SHXmpIfZbCDKQkOJWBoFKF7362vtR6/RsQR+/pk1iU347itR/snmnz7cQ5iYSwyVtzJ6VSE847PvXL9CBon+xG2JM9azO32aLGRBaOgeXZx4/Qck7T6jUKpIVJnSXZDyubjqys/c8cKfNp0hEmJJdjPJ/S40soUPMbYbzzr9O3GawwiosjTvRZt0Dy0VthzMbCWKChqJOS1IVyjP+7cv0F9Ff1J2JKjc6k8z8MEpNxoRyO3//xAAdEQEAAwACAwEAAAAAAAAAAAABAAIQICESMTJB/9oACAECAQE/AMK2fyNbB64UBdt1Z2n1t/rRTGKugsOiMRMKV4+BP//EABsRAQACAwEBAAAAAAAAAAAAAAEAEAISISAx/9oACAEDAQE/AK2IZD4zULx+Xn8vB5aDR1gBexHrDjDIa3fO7P/Z",
                    AccountType = "internal"
                };

                await ux.SaveAsync();
                ux.Roles.Add(adminRole);                                                                        
                                
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw;
            }
        }

        internal object GetAllRoles()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// get user photo.
        /// </summary>
        public async Task<string> UserPhoto(string id)
        {
            try
            {
                return (await DB.Find<UsersModel>().OneAsync(id)).Photo;                
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw;
            }
        }

          /// <summary>
        /// get user photo.
        /// </summary>
        public async Task<UsersModel> FindUserByEmail(string email)
        {
            try
            {                
                return (await DB.Find<UsersModel>().Match(a => a.Email == email.Trim().ToLower()).ExecuteAsync()).FirstOrDefault();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw;
            }
        }

         public async Task<List<RolesModel>> GetAllRoles(string search)
        {
            try
            {                
                return (await DB.Find<RolesModel>().Match(z=>z.Role.Contains(search)).ExecuteAsync()).ToList();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw;
            }
        }
                        
            
    }
}