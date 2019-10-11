using Base.Models.Base;
using Base.Models.Settings;
using Microsoft.EntityFrameworkCore;

//
//     Database migrations
//     https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/new-db?tabs=netcore-cli
//
//        dotnet ef migrations add InitialSetup
//        dotnet ef database update
//


namespace Base.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<UsersModel> Users { get; set; }
        public DbSet<RolesModel> Roles { get; set; }
        public DbSet<OrganizationModel> Organization { get; set; }
    }
}