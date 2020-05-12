using System;
using eProject.Models;
using Microsoft.AspNetCore.Identity;

namespace eProject.App.Data.SeedData
{
    public static class IdentityDataInitializer
    {
        public static void SeedData
        (UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers
        (UserManager<User> userManager)
        {

            if (userManager.FindByNameAsync
            ("admin@gmail.com").Result == null)
            {
                User user = new User
                {
                    FirstName = "Trinh Hao",
                    LastName = "Hiep",
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    PhoneNumber = "0909090909",
                    Gender = User.GenderType.Male,
                    DateOfBirthDay = new DateTime(1999, 7, 26),
                    Address = new Address
                    {
                        StreetAddress = "123 Pham Hung",
                        PostalCode = "80000",
                        City = "Ho Chi Minh",
                        County = "Binh Chanh",
                        State = "Viet Nam"
                    }
                };

                IdentityResult result = userManager.CreateAsync(user, "123456!Aa").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Admin").Wait();
                }
            }
        }

        public static void SeedRoles
        (RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync
                ("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync
                ("Artis").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Artis";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync
                ("Customer").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Customer";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
    }
}
