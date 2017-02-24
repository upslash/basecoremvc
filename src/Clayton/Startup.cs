using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Clayton.Models;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Clayton
{
    public class Startup
    {
        private IConfigurationRoot _configurationRoot;
        private IHostingEnvironment _currentEnvironment;

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            // Setup configuration file so we can 
            // create our DB context later
            _configurationRoot = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();

            // Persist current environment
            _currentEnvironment = hostingEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            // Add DB context to our database
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(_configurationRoot.GetConnectionString("DefaultConnection")));

            // Add support for ASP.Net Identity & use database from AppDbContext
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Disable password complexity requirements in development
                // http://stackoverflow.com/questions/32548948/how-to-get-the-development-staging-production-hosting-environment-in-configurese
                if (_currentEnvironment.IsDevelopment())
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                }
                
            });

            // Repositories
            // We can switch between mock repo and real here.
            // Because of dependency injection, we don't have to go through our entire app
            // and change references if we want to use MockData vs. Real data.
            // services.AddTransient<IPostRepository, MockPostRepository>();
            services.AddTransient<IPostRepository, PostRepository>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();

            // Be sure to call app.Useidentity() BEFORE app.UseMvc().
            // Otherwise you might get an error message:
            // InvalidOperationException: No authentication handler is configured to handle the scheme: Identity.Application
            // http://stackoverflow.com/questions/38968422/no-authentication-handler-is-configured-to-handle-the-scheme

            app.UseIdentity(); 
            app.UseMvcWithDefaultRoute(); // Sets up MVC middleware with default schema. Routing would need to be setup.

            DbInitializer.Seed(app); // Seed data if needed
        }
    }
}
