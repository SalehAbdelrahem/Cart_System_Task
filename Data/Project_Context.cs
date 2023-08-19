using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Reflection.Emit;
using static System.Net.Mime.MediaTypeNames;

namespace Data
{
    public class Project_Context : IdentityDbContext<User, Role, int>
    {
        public Project_Context(DbContextOptions<Project_Context> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(new Role {Id=1, Name = "User", NormalizedName = "User".ToUpper() });
            builder.Entity<Role>().HasData(new Role { Id =2, Name = "Admin", NormalizedName = "Admin".ToUpper() });
            var password = new PasswordHasher<User>();
            builder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name ="Admin",
                    Email = "Admin@gmail.com",
                    PasswordHash = password.HashPassword(null, "Test@123"),
                    NormalizedEmail = "Admin@gmail.com".ToUpper(),
                    EmailConfirmed = true,
                    NormalizedUserName = "Admin@gmail.com".ToUpper(),
                    PhoneNumber = "01026691741".ToUpper(),
                    PhoneNumberConfirmed = true,
                    UserName = "Admin@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
       
                });
            builder.Entity<IdentityUserRole<int>>().HasData(
            new IdentityUserRole<int>
            {
                RoleId = 2,
                UserId = 1
            });
            base.OnModelCreating(builder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Images> Images { get; set; }


    }
}