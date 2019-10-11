using System;
using System.Linq;
using System.Threading.Tasks;
using Base.Models.Settings;
using MongoDB.Entities;
using Serilog;

namespace Base.DataAccess
{
    public class SettingsDataAccess
    {       

        public async Task<OrganizationModel> Organization()
        {
            try
            {
                return (await DB.Find<OrganizationModel>().ExecuteAsync()).FirstOrDefault() ?? new OrganizationModel();
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw;
            }
        }

    }
}