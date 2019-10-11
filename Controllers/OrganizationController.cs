using Base.DataAccess;
using Base.Models.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Base.Controllers
{
    [Route("api/[controller]")]
    public class OrganizationController: Controller
    {

       [HttpGet]
       
       [Route("get")]
        public async Task<OrganizationModel> GetOrganization()
        {

            var sd= new SettingsDataAccess();
            return await sd.Organization();                        

        }
    }
}
