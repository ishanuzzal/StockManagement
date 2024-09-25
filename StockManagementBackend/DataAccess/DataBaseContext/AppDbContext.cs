using DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;


namespace DataAccess.DataBaseContext
{
    public class AppDbContext: IdentityDbContext
    {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {

            }




            public DbSet<Products> Products { get; set; }
            public DbSet<Users> AppUsers { get; set; }
            public DbSet<Transactions> Transactions { get; set; }
            public DbSet<Categories> Categories { get; set; }

            protected override void OnModelCreating(ModelBuilder builder)
            {
            base.OnModelCreating(builder); 

            // Seed initial roles
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "manager",
                    NormalizedName = "MANAGER"
                },
                new IdentityRole
                {
                    Name = "user",
                    NormalizedName = "USER"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
