using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.EntityFrameworkCore.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            const string readerRoleId = "60cfaf67-78b0-4885-a9f4-0e20f73cef92";
            const string writerRoleId = "d1c3f8b2-4e5a-4f6b-8c7d-9e0f1a2b3c4d";

            // Create reader and writer roles

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    Name = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId,
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    Name = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            // Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);


            // Create admin user

            const string adminUserId = "aaa39788-f536-4e27-86da-deba74c448c1";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@codepulse.com",
                Email = "admin@codepulse.com",
                NormalizedEmail = "admin@codepulse.com".ToUpper(),
                NormalizedUserName = "admin@codepulse.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin12@");

            builder.Entity<IdentityUser>().HasData(admin);

            // Give roles to admin

            var adminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new IdentityUserRole<string>
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                }
            };


            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }


    }
}
