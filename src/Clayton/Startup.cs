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

namespace Clayton
{
    public class Startup
    {
        private IConfigurationRoot _configurationRoot;

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            // Setup configuration file so we can 
            // create our DB context later
            _configurationRoot = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {

            // Add DB context to our database
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(_configurationRoot.GetConnectionString("DefaultConnection")));

            // Repositories
            // We can switch between mock repo and real here. 
            // No need to go through our entire app and change.
            // services.AddTransient<IPostRepository, MockPostRepository>();
            services.AddTransient<IPostRepository, PostRepository>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles(); 
            app.UseMvcWithDefaultRoute(); // Sets up MVC middleware with default schema. Routing would need to be setup.

            DbInitializer.Seed(app); // Check if data exists, seed if it doesn't.
        }
    }
}
