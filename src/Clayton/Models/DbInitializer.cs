using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clayton.Models
{
    public static class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            AppDbContext context =
                applicationBuilder.ApplicationServices.GetRequiredService<AppDbContext>();

            if (!context.Posts.Any())
            {
                context.AddRange
                (
                    new Post() { Name = "Today I learned ASP.NET", Content = "On a sunny day, in the year 2017, Clayton brought it opon himself to learn MVC." },
                    new Post() { Name = "Today I learned Linux", Content = "On a clody day, in the year 2016, Clayton decided to learn Linux." },
                    new Post() { Name = "Today I learned iOS Development", Content = "On a stormy day, in the year 2015, Clayton made the terrible choice is learning iOS." }
                );

                context.SaveChanges();
            }
        }
    }
}
