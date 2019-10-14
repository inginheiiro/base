
using Base.Custom;
using Base.Settings.Email;
using Base.Settings.Email.Interfaces;
using JWTSimpleServer.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Entities;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace Base
{
    public class Startup
    {
        
        public const string SigningKey = "InMemorySampleSigningKey";

         private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {                                    
            

            services.AddSingleton<IAuthenticationProvider, JwtAuthenticationProvider>()
            .AddCors()
            .AddJwtSimpleServer(setup =>
            {                                
                setup.IssuerSigningKey = SigningKey;
            })
            .AddJwtInMemoryRefreshTokenStore()
            .AddMvcCore()
            .AddNewtonsoftJson()            
            .AddApiExplorer()
            .AddAuthorization();                       
            services.AddResponseCompression();
                                                                 
                                    
            services.AddSingleton(Configuration);     
            
            services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("MailConfiguration").Get<EmailConfiguration>());
            services.AddTransient<IEmailService, EmailService>();



            services.AddMongoDBEntities(Configuration.GetSection("MongoConnection:Database").Value,Configuration.GetSection("MongoConnection:ConnectionString").Value,27017);

            
            /*
            var connectionString = Configuration["ConnectionStrings:SqlServer"];
            services.AddDbContext<DatabaseContext>(opts => opts.UseSqlServer(connectionString));
            */
            
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {Title = "WP API", Version = "v1"}); });
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();                        

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "WP API V1"); });
           

            app.UseStaticFiles();
            app.UseWebSockets();
            app.UseResponseCompression();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());


            app.UseJwtSimpleServer(setup =>
                {
                    setup.IssuerSigningKey = SigningKey;
                }
            );

           app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });
        }
    }
}