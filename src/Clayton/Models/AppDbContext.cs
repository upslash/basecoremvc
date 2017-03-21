using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clayton.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Enhancement> Enhancements { get; set; }

        public DbSet<EnhancementProgress> EnhancementProgress { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // DbSet names are now the table name unless you do this
            // http://stackoverflow.com/questions/37493095/entity-framework-core-rc2-table-name-pluralization
            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<Category>().ToTable("Category");

            // I'd like to test at some point if this line would do the same thing as the lines
            // below it. I saw it in one of the articles examples.
            //modelBuilder.Entity<PostCategory>().HasKey(x => new { x.PostId, x.CategoryId });

            modelBuilder.Entity<PostCategory>()
                .HasKey(t => new { t.PostId, t.CategoryId });

            modelBuilder.Entity<PostCategory>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostCategory)
                .HasForeignKey(pt => pt.PostId);

            modelBuilder.Entity<PostCategory>()
                .HasOne(pt => pt.Category)
                .WithMany(t => t.PostCategory)
                .HasForeignKey(pt => pt.CategoryId);

            // Make sure to include this line here. 
            // Otherwise you'll get an error. 
            // http://stackoverflow.com/questions/31122057/onmodelcreating-fails-when-context-inherits-from-identitydbcontext

            base.OnModelCreating(modelBuilder);
        }


    }
}
