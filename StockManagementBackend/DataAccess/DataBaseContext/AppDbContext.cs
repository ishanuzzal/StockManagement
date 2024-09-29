using DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Reflection.Emit;


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

            public DbSet<BussinessEntities> BussinessEntities { get; set; }

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


            builder.Entity<Products>()
                .HasOne(p => p.User)
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Products>()
                .HasOne(p => p.Categories)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoriesId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Transactions>()
                .HasOne(t => t.Users)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Transactions>()
                .HasOne(t => t.Products)
                .WithMany(p => p.Transactions)
                .HasForeignKey(t => t.ProductsId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Transactions>()
           .HasOne(t => t.BussinessEntities)
           .WithMany(be => be.Transactions)
           .HasForeignKey(t => t.BussinessEntitiesId)
           .OnDelete(DeleteBehavior.NoAction);




            builder.Entity<Categories>()
                .HasOne(c => c.Users)
                .WithMany()
                .HasForeignKey(c => c.UsersId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<BussinessEntities>()
           .Property(be => be.Type)
           .HasConversion<string>();
        }
    }
}
