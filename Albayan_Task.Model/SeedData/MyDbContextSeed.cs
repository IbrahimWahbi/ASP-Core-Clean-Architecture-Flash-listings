using Albayan_Task.Domain.Entities.Categories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albayan_Task.Model.SeedData
{
    public class MyDbContextSeed
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<IdentityRole>().HasData(
                   new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" }                
           );


            var user = new IdentityUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                UserName = "admin",
                NormalizedUserName = ("Admin").ToUpper(),
                Email = "admin@gmail.com",
                NormalizedEmail = ("admin@gmail.com").ToUpper(),
                LockoutEnabled = false,
                PhoneNumber = "1234567890"
            };
            PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "Im$trongPassw0rd");
            modelBuilder.Entity<IdentityUser>().HasData(user);


            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
               new IdentityUserRole<string>() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" }
               );
            var categories = new List<Category>();
            categories.Add(new Category()
            {
                Id = 1,
                EnglishName= "Cars",
                ArabicName="سيارات"
            });
            categories.Add(new Category()
            {
                Id = 2,
                EnglishName = "clothes",
                ArabicName = "ملابس"
            });
            categories.Add(new Category()
            {
                Id = 3,
                EnglishName = "Food",
                ArabicName = "طعام"
            });

            modelBuilder.Entity<Category>().HasData(categories);

        }
    }
}
