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
            SeedBlogs(applicationDbContext, userManager);
            SeedContacts(applicationDbContext);
            SeedWareHouseAddress(applicationDbContext);
            SeedAbouts(applicationDbContext);
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
                    Price = 3500,
                    SalePrice = 100,
                    Quantity = 1,
                    Hot = 0,
                    FromDate = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 1,
                    Description = "Newly created, consigned directly from the artist. Condition report and certificate of authenticity available upon request.",
                    Status = true,
                    Featured = true,
                    Auction = false,
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Foreseeing Nothing",
                    Price = 12500,
                    SalePrice = 100,
                    Quantity = 1,
                    Hot = 0,
                    FromDate = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 1,
                    Description = "Newly created, consigned directly from the artist. Condition report and certificate of authenticity available upon request.",
                    Status = true,
                    Featured = true,
                    Auction = false,
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Idle Hands",
                    Price = 12500,
                    SalePrice = 100,
                    Quantity = 1,
                    Hot = 0,
                    FromDate = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 1,
                    Description = "Newly created, consigned directly from the artist. Condition report and certificate of authenticity available upon request.",
                    Status = true,
                    Featured = true,
                    Auction = false,
                },
                new Product
                {
                    ProductId = 4,
                    Name = "If He Wasn't",
                    Price = 12500,
                    SalePrice = 0,
                    Quantity = 1,
                    Hot = 0,
                    FromDate = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 1,
                    Description = "Newly created, consigned directly from the artist. Condition report and certificate of authenticity available upon request.",
                    Status = true,
                    Featured = true,
                    Auction = false,
                },
                new Product
                {
                    ProductId = 5,
                    Name = "What Once Was",
                    Price = 12500,
                    SalePrice = 0,
                    Quantity = 1,
                    Hot = 0,
                    FromDate = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 1,
                    Description = "Newly created, consigned directly from the artist. Condition report and certificate of authenticity available upon request.",
                    Status = true,
                    Featured = true,
                    Auction = false,
                },
                new Product
                {
                    ProductId = 6,
                    Name = "Badain",
                    Price = 30000,
                    SalePrice = 0,
                    Quantity = 1,
                    Hot = 0,
                    FromDate = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 2,
                    Description = "American photographer Aaron Young won first place in the Landscape Photographer of the Year category for four photos taken in the Badain Jaran desert in Iceland.",
                    Status = true,
                    Featured = true,
                    Auction = false,
                },
                new Product
                {
                    ProductId = 7,
                    Name = "Bláfellsá",
                    Price = 30000,
                    SalePrice = 30000,
                    Quantity = 1,
                    Hot = 0,
                    FromDate = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 2,
                    Description = "The International Landscape Photographer of the Year contest has just announced its 2019 winners, and their pics are the perfect reminders of just how diverse and beautiful our Mother Earth really is.",
                    Status = true,
                    Featured = true,
                    Auction = true,
                },
                new Product
                {
                    ProductId = 8,
                    Name = "Fleswich bay",
                    Price = 30000,
                    SalePrice = 30000,
                    Quantity = 1,
                    Hot = 0,
                    FromDate = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 2,
                    Description = "The International Landscape Photographer of the Year contest has just announced its 2019 winners, and their pics are the perfect reminders of just how diverse and beautiful our Mother Earth really is.",
                    Status = true,
                    Featured = true,
                    Auction = true,
                },
                new Product
                {
                    ProductId = 9,
                    Name = "Haifoss",
                    Price = 30000,
                    SalePrice = 30000,
                    Quantity = 1,
                    Hot = 0,
                    FromDate = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 2,
                    Description = "Ershov believes that a picture is beautiful when hung on a wall, so he focuses on creating special photographs like large-sized wall paintings.",
                    Status = true,
                    Featured = true,
                    Auction = true,
                },
                new Product
                {
                    ProductId = 10,
                    Name = "K Muffarfjoll",
                    Price = 30000,
                    SalePrice = 30000,
                    Quantity = 1,
                    Hot = 0,
                    FromDate = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 2,
                    Description = "This Russia. He has been to Iceland 15 times and took 10 years to complete the first book of his career.",
                    Status = true,
                    Featured = true,
                    Auction = true,
                },
                new Product
                {
                    ProductId = 11,
                    Name = "Last Call",
                    Price = 1800,
                    SalePrice = 1800,
                    Quantity = 1,
                    Hot = 0,
                    FromDate = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 3,
                    Description = "Assorted plastics, acrylic paint",
                    Status = true,
                    Featured = true,
                    Auction = true,
                },
                new Product
                {
                    ProductId = 12,
                    Name = "Merry Woodsman",
                    Price = 1500,
                    SalePrice = 1500,
                    Quantity = 1,
                    Hot = 0,
                    FromDate = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 3,
                    Description = "Assorted plastics, acrylic paint",
                    Status = true,
                    Featured = true,
                    Auction = true,
                },
                new Product
                {
                    ProductId = 13,
                    Name = "Oh Dear",
                    Price = 1500,
                    SalePrice = 1500,
                    Quantity = 1,
                    Hot = 0,
                    FromDate = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 3,
                    Description = "Assorted plastics, acrylic paint",
                    Status = true,
                    Featured = false,
                    Auction = true,
                },
                new Product
                {
                    ProductId = 14,
                    Name = "Speed Dating",
                    Price = 1500,
                    SalePrice = 1500,
                    Quantity = 1,
                    Hot = 0,
                    FromDate = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 3,
                    Description = "Assorted plastics, acrylic paint",
                    Status = true,
                    Featured = false,
                    Auction = true,
                },
                new Product
                {
                    ProductId = 15,
                    Name = "We Can Dream",
                    Price = 1500,
                    SalePrice = 1500,
                    Quantity = 1,
                    Hot = 0,
                    FromDate = DateTime.Now,
                    UserId = user.Id,
                    CategoryId = 3,
                    Description = "Assorted plastics, acrylic paint",
                    Status = true,
                    Featured = false,
                    Auction = true,
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
                    Name = "Badain.png",
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
                    Title = "Title SlideShow",
                    Description = "50%"
                },
                new SlideShow
                {
                    Id = 2,
                    Name = "banner2.jpg",
                    Status = true,
                    Title = "Title SlideShow",
                    Description = "10%"
                },
                new SlideShow
                {
                    Id = 3,
                    Name = "banner3.jpg",
                    Status = true,
                    Title = "Title SlideShow",
                    Description = "20%"
                },
            };

            applicationDbContext.SlideShows.AddRange(slideShows);
            applicationDbContext.SaveChanges();
        }

        private static void SeedBlogs(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
        {
            User user = userManager.FindByEmailAsync("admin@gmail.com").Result;

            List<Blog> blogs = new List<Blog>
            {
                new Blog
                {
                    BlogId = 1,
                    Title = "DIFFERENCE BETWEEN TEMPERA",
                    Photo = "blog1.jpg",
                    PostedDate = DateTime.Now,
                    UserId = user.Id,
                    Description = "Tempera is a color bound by a sticky binder or by egg yolk. In the European tradition...",
                    Content =  "Tempera is a color bound by a sticky binder or by egg yolk. In the European tradition it is opposed to oil painting, with its lower, dimmed and less shiny...",

                },
                new Blog
                {
                    BlogId = 2,
                    Title = "USE OF VARNISH IN PAINTINGS",
                    Photo = "blog2.jpg",
                    PostedDate = DateTime.Now,
                    UserId = user.Id,
                    Description = "Originally, the varnish used for the paintings was a resin dissolved in oil. It was ...",
                    Content =  "Tempera is a color bound by a sticky binder or by egg yolk. In the European tradition it is opposed to oil painting, with its lower, dimmed and less shiny...",
                },
                new Blog
                {
                    BlogId = 3,
                    Title = "USE OF GOLD IN PAINTING",
                    Photo = "blog3.jpg",
                    PostedDate = DateTime.Now,
                    UserId = user.Id,
                    Description = "The use of gold in painting is linked to the recognition of the role of art of ...",
                    Content =  "Tempera is a color bound by a sticky binder or by egg yolk. In the European tradition it is opposed to oil painting, with its lower, dimmed and less shiny...",
                },
                new Blog
                {
                    BlogId = 4,
                    Title = "THE ART OF PAINTING AND DREAMS",
                    Photo = "blog4.jpg",
                    PostedDate = DateTime.Now,
                    UserId = user.Id,
                    Description = "It is impressive that painting, which is the most sensual of the arts, is also...",
                    Content = "It is impressive that painting, which is the most sensual of the arts, is also the most metaphysical. For color, of course, but not only for that",
                },
                new Blog
                {
                    BlogId = 5,
                    Title = "WHY COPIES, REPRODUCTIONS AND FAKES PAINTINGS?",
                    Photo = "blog5.jpg",
                    PostedDate = DateTime.Now,
                    UserId = user.Id,
                    Description = "It usually happens that the original paintings produced by the artist could ...",
                    Content = "WHY COPIES, REPRODUCTIONS AND FAKES PAINTINGS?",
                },
            };

            applicationDbContext.Blog.AddRange(blogs);
            applicationDbContext.SaveChanges();
        }

        private static void SeedContacts(ApplicationDbContext applicationDbContext)
        {
            Contact contact = new Contact
            {
                Id = 1,
                Address = "590 CMT8, District 3, HCMC, Vietnam",
                Telephone = 0909090909,
                Fax = 0909090909,
                Email = "admin@gmail.com"
            };

            applicationDbContext.Contacts.Add(contact);
            applicationDbContext.SaveChanges();
        }

        private static void SeedWareHouseAddress(ApplicationDbContext applicationDbContext)
        {
            WareHouseAddress ware = new WareHouseAddress()
            {
                WareHouseId = 1,
                CompanyName = "Heaven Art",
                StreetAddress = "590 CMT8, District 3, HCMC, Vietnam",
                PostalCode = "70000",
                PhoneNumber = "0909123456",
                Email = "admin@gmail.com"
            };

            applicationDbContext.WareHouseAddresses.Add(ware);
            applicationDbContext.SaveChanges();
        }

        private static void SeedAbouts(ApplicationDbContext applicationDbContext)
        {
            List<About> abouts = new List<About>
            {
                new About
                {
                    AboutId = 1,
                    Image = "nguyennhathoangha.jpg",
                    Name = "Nguyen Nhat Hoang Ha",
                    Description = "Member",
                    Slogan = "“if it were easy everybody would do it."
                },
                new About
                {
                    AboutId = 2,
                    Image = "trinhhaohiep.jpg",
                    Name = "Trinh Hao Hiep",
                    Description = "Member",
                    Slogan = "“I can actually do this!"
                },
                new About
                {
                    AboutId = 3,
                    Image = "phankhactrieu.jpg",
                    Name = "Phan Khac Trieu",
                    Description = "Member",
                    Slogan = "“Failure teaches me."
                },
                new About
                {
                    AboutId = 4,
                    Image = "tranquangtuyen.jpg",
                    Name = "Tran Quang Tuyen",
                    Description = "Member",
                    Slogan = "“My work matters."
                },
                new About
                {
                    AboutId = 5,
                    Image = "todaoviethoang.jpg",
                    Name = "To Dao Viet Hoang",
                    Description = "Leader",
                    Slogan = " “Other people’s success inspires me"
                },
            };

            applicationDbContext.About.AddRange(abouts);
            applicationDbContext.SaveChanges();
        }
    }
}
