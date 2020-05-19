using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using eProject.Data;
using eProject.Models;
using Microsoft.AspNetCore.Identity;

namespace eProject.App.Data.SeedData
{
    public static class DataInitializer
    {
        public static void SeedData(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
        {
            SeedCategories(applicationDbContext);
            SeedProducts(applicationDbContext, userManager);
            SeedPhotos(applicationDbContext);
            SeedSlideShows(applicationDbContext);
        }

        private static void SeedCategories(ApplicationDbContext applicationDbContext)
        {
            List<Category> categories = new List<Category>
            {
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Painting",
                    Status = true
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Photography",
                    Status = true
                },
                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Sculpture",
                    Status = true
                }
            };

            applicationDbContext.Categories.AddRange(categories);
            applicationDbContext.SaveChanges();
        }

        private static void SeedProducts(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
        {
            User user = userManager.FindByEmailAsync("artist@gmail.com").Result;

            List<Product> products = new List<Product>
            {
                new Product
                {
                    ProductId = 1,
                    Name = "A Constant",
                    Price = 3.500,
                    SalePrice = 0,
                    Quantity = 1,
                    Hot = 0,
                    Created_At = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 1,
                    Description = "Newly created, consigned directly from the artist. Condition report and certificate of authenticity available upon request.",
                    Status = true,
                    Featured = true
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Foreseeing Nothing",
                    Price = 12.500,
                    SalePrice = 0,
                    Quantity = 1,
                    Hot = 0,
                    Created_At = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 1,
                    Description = "Newly created, consigned directly from the artist. Condition report and certificate of authenticity available upon request.",
                    Status = true,
                    Featured = true
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Idle Hands",
                    Price = 12.500,
                    SalePrice = 0,
                    Quantity = 1,
                    Hot = 0,
                    Created_At = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 1,
                    Description = "Newly created, consigned directly from the artist. Condition report and certificate of authenticity available upon request.",
                    Status = true,
                    Featured = true
                },
                new Product
                {
                    ProductId = 4,
                    Name = "If He Wasn't Handsome",
                    Price = 12.500,
                    SalePrice = 0,
                    Quantity = 1,
                    Hot = 0,
                    Created_At = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 1,
                    Description = "Newly created, consigned directly from the artist. Condition report and certificate of authenticity available upon request.",
                    Status = true,
                    Featured = true
                },
                new Product
                {
                    ProductId = 5,
                    Name = "In Search of What Once Was",
                    Price = 12.500,
                    SalePrice = 0,
                    Quantity = 1,
                    Hot = 0,
                    Created_At = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 1,
                    Description = "Newly created, consigned directly from the artist. Condition report and certificate of authenticity available upon request.",
                    Status = true,
                    Featured = true
                },
                new Product
                {
                    ProductId = 6,
                    Name = "Badain",
                    Price = 30.000,
                    SalePrice = 0,
                    Quantity = 1,
                    Hot = 0,
                    Created_At = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 2,
                    Description = "American photographer Aaron Young won first place in the Landscape Photographer of the Year category for four photos taken in the Badain Jaran desert in Iceland.",
                    Status = true,
                    Featured = true
                },
                new Product
                {
                    ProductId = 7,
                    Name = "Bláfellsá",
                    Price = 30.000,
                    SalePrice = 0,
                    Quantity = 1,
                    Hot = 0,
                    Created_At = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 2,
                    Description = "The International Landscape Photographer of the Year contest has just announced its 2019 winners, and their pics are the perfect reminders of just how diverse and beautiful our Mother Earth really is.",
                    Status = true,
                    Featured = true
                },
                new Product
                {
                    ProductId = 8,
                    Name = "Fleswich bay",
                    Price = 30.000,
                    SalePrice = 0,
                    Quantity = 1,
                    Hot = 0,
                    Created_At = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 2,
                    Description = "The International Landscape Photographer of the Year contest has just announced its 2019 winners, and their pics are the perfect reminders of just how diverse and beautiful our Mother Earth really is.",
                    Status = true,
                    Featured = true
                },
                new Product
                {
                    ProductId = 9,
                    Name = "Haifoss",
                    Price = 30.000,
                    SalePrice = 0,
                    Quantity = 1,
                    Hot = 0,
                    Created_At = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 2,
                    Description = "Ershov believes that a picture is beautiful when hung on a wall, so he focuses on creating special photographs like large-sized wall paintings.",
                    Status = true,
                    Featured = true
                },
                new Product
                {
                    ProductId = 10,
                    Name = "K Muffarfjoll",
                    Price = 30.000,
                    SalePrice = 0,
                    Quantity = 1,
                    Hot = 0,
                    Created_At = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 2,
                    Description = "This Russia. He has been to Iceland 15 times and took 10 years to complete the first book of his career.",
                    Status = true,
                    Featured = true
                },
                new Product
                {
                    ProductId = 11,
                    Name = "Last Call",
                    Price = 1.800,
                    SalePrice = 0,
                    Quantity = 1,
                    Hot = 0,
                    Created_At = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 3,
                    Description = "Assorted plastics, acrylic paint",
                    Status = true,
                    Featured = true
                },
                new Product
                {
                    ProductId = 12,
                    Name = "Merry Woodsman",
                    Price = 1.500,
                    SalePrice = 0,
                    Quantity = 1,
                    Hot = 0,
                    Created_At = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 3,
                    Description = "Assorted plastics, acrylic paint",
                    Status = true,
                    Featured = true
                },
                new Product
                {
                    ProductId = 13,
                    Name = "Oh Dear",
                    Price = 1.500,
                    SalePrice = 0,
                    Quantity = 1,
                    Hot = 0,
                    Created_At = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 3,
                    Description = "Assorted plastics, acrylic paint",
                    Status = true,
                    Featured = true
                },
                new Product
                {
                    ProductId = 14,
                    Name = "Speed Dating",
                    Price = 1.500,
                    SalePrice = 0,
                    Quantity = 1,
                    Hot = 0,
                    Created_At = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 3,
                    Description = "Assorted plastics, acrylic paint",
                    Status = true,
                    Featured = true
                },
                new Product
                {
                    ProductId = 15,
                    Name = "We Can Dream",
                    Price = 1.500,
                    SalePrice = 0,
                    Quantity = 1,
                    Hot = 0,
                    Created_At = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 3,
                    Description = "Assorted plastics, acrylic paint",
                    Status = true,
                    Featured = true
                },
            };

            applicationDbContext.Products.AddRange(products);
            applicationDbContext.SaveChanges();
        }

        private static void SeedPhotos(ApplicationDbContext applicationDbContext)
        {
            List<Photo> photos = new List<Photo>
            {
                new Photo
                {
                    PhotoId = 1,
                    Name = "AConstant.png",
                    ProductId = 1,
                    Status = true,
                    Featured = true
                },
                new Photo
                {
                    PhotoId = 2,
                    Name = "ForeseeingNothing.png",
                    ProductId = 2,
                    Status = true,
                    Featured = true
                },
                new Photo
                {
                    PhotoId = 3,
                    Name = "IdleHands.png",
                    ProductId = 3,
                    Status = true,
                    Featured = true
                },
                new Photo
                {
                    PhotoId = 4,
                    Name = "IfHeWasntHandsome.png",
                    ProductId = 4,
                    Status = true,
                    Featured = true
                },
                new Photo
                {
                    PhotoId = 5,
                    Name = "InSearchofWhatOnceWas.png",
                    ProductId = 5,
                    Status = true,
                    Featured = true
                },
                new Photo
                {
                    PhotoId = 6,
                    Name = "Badain",
                    ProductId = 6,
                    Status = true,
                    Featured = true
                },
                new Photo
                {
                    PhotoId = 7,
                    Name = "Blafellsa.png",
                    ProductId = 7,
                    Status = true,
                    Featured = true
                },
                new Photo
                {
                    PhotoId = 8,
                    Name = "Fleswichbay.png",
                    ProductId = 8,
                    Status = true,
                    Featured = true
                },
                new Photo
                {
                    PhotoId = 9,
                    Name = "Haifoss.png",
                    ProductId = 9,
                    Status = true,
                    Featured = true
                },
                new Photo
                {
                    PhotoId = 10,
                    Name = "KMuffarfjoll.png",
                    ProductId = 10,
                    Status = true,
                    Featured = true
                },
                new Photo
                {
                    PhotoId = 11,
                    Name = "LastCall.png",
                    ProductId = 11,
                    Status = true,
                    Featured = true
                },
                new Photo
                {
                    PhotoId = 12,
                    Name = "MerryWoodsman.png",
                    ProductId = 12,
                    Status = true,
                    Featured = true
                },
                new Photo
                {
                    PhotoId = 13,
                    Name = "OhDear.png",
                    ProductId = 13,
                    Status = true,
                    Featured = true
                },
                new Photo
                {
                    PhotoId = 14,
                    Name = "SpeedDating.png",
                    ProductId = 14,
                    Status = true,
                    Featured = true
                },
                new Photo
                {
                    PhotoId = 15,
                    Name = "WeCanDream.png",
                    ProductId = 15,
                    Status = true,
                    Featured = true
                },
            };

            applicationDbContext.Photos.AddRange(photos);
            applicationDbContext.SaveChanges();
        }

        private static void SeedSlideShows(ApplicationDbContext applicationDbContext)
        {
            List<SlideShow> slideShows = new List<SlideShow>
            {
                new SlideShow
                {
                    Id = 1,
                    Name = "banner1.jpg",
                    Status = true,
                    Title = "Title Slide Show 1",
                    Description = "Description Slide Show 1"
                },
                new SlideShow
                {
                    Id = 2,
                    Name = "banner2.jpg",
                    Status = true,
                    Title = "Title Slide Show 2",
                    Description = "Description Slide Show 2"
                },
                new SlideShow
                {
                    Id = 3,
                    Name = "banner3.jpg",
                    Status = true,
                    Title = "Title Slide Show 3",
                    Description = "Description Slide Show 3"
                },
            };

            applicationDbContext.SlideShows.AddRange(slideShows);
            applicationDbContext.SaveChanges();
        }
    }
}
