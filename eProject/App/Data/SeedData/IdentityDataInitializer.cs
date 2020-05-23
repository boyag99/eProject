using System;
using System.Collections.Generic;
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
            ("admin@gmail.com").Result == null &&
            userManager.FindByNameAsync
            ("artist@gmail.com").Result == null &&
            userManager.FindByNameAsync
            ("customer@gmail.com").Result == null)
            {

                List<User> users = new List<User>
                {
                    new User
                    {
                        FirstName = "To Dao",
                        LastName = "Viet Hoang",
                        UserName = "customer@gmail.com",
                        Email = "customer@gmail.com",
                        PhoneNumber = "0909090909",
                        Gender = User.GenderType.Male,
                        DateOfBirthDay = new DateTime(1999, 7, 26),
                        Address = new Address
                        {
                            StreetAddress = "123 Pham Hung",
                            PostalCode = "80000",
                            City = "Ho Chi Minh",
                            County = "Binh Chanh",
                            Country = "Vietnam"
                        },
                        EmailConfirmed = true
                    },
                    new User
                    {
                        FirstName = "Tran Quang",
                        LastName = "Tuyen",
                        UserName = "artist@gmail.com",
                        Email = "artist@gmail.com",
                        PhoneNumber = "0909090909",
                        Gender = User.GenderType.Male,
                        DateOfBirthDay = new DateTime(1999, 7, 26),
                        Address = new Address
                        {
                            StreetAddress = "123 Pham Hung",
                            PostalCode = "80000",
                            City = "Ho Chi Minh",
                            County = "Binh Chanh",
                            Country = "Vietnam"
                        },
                        EmailConfirmed = true
                    },
                    new User
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
                            Country = "Vietnam"
                        },
                        EmailConfirmed = true
                    }
                };

                foreach (var user in users)
                {
                    IdentityResult result = userManager.CreateAsync(user, "123456!Aa").Result;

                    if (result.Succeeded)
                    {
                        switch (user.Email)
                        {
                            case "admin@gmail.com":
                                userManager.AddToRoleAsync(user,
                                            "Admin").Wait();
                                break;
                            case "artist@gmail.com":
                                userManager.AddToRoleAsync(user,
                                            "Artist").Wait();
                                break;
                            case "customer@gmail.com":
                                userManager.AddToRoleAsync(user,
                                            "Customer").Wait();
                                break;
                        }
                    }
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
                ("Artist").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Artist";
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
