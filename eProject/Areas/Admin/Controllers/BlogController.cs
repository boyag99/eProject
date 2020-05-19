using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eProject.Models;
using Microsoft.AspNetCore.Authorization;
using eProject.Data;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace eProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/Blog")]
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<User> _userManager;
        //private readonly IHostEnvironment Environment;
        public BlogController(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;

        }

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            List<Blog> data = _applicationDbContext.Blog
                .Include(b => b.User)
                .ToList();
            return View(data);
        }


        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var blog = new Blog();
            return View("Create", blog);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Blog blog, IFormFile photo)
        {
            User user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                if (photo.Length > 0)
                {
                    var path = Path.Combine("wwwroot/blog", photo.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    await photo.CopyToAsync(stream);

                    blog.Photo = "/blog/" + photo.FileName;
                    blog.UserId = user.Id;
                    _applicationDbContext.Blog.Add(blog);
                    _applicationDbContext.SaveChanges();
                    return RedirectToAction("Index", "Blog", new { area = "admin" });
                }

            }

            return View();
        }



        [HttpGet]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {

            Blog blog = _applicationDbContext.Blog.SingleOrDefault(a => a.BlogId == id);

            _applicationDbContext.Blog.Remove(blog);
            _applicationDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var blog = _applicationDbContext.Blog.Find(id);
            return View("Edit", blog);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, Blog blog, IFormFile photo)
        {
            var currentBlogContent = _applicationDbContext.Blog.Find(id);
            if (photo != null && !string.IsNullOrEmpty(photo.FileName))
            {
                var path = Path.Combine("wwwroot/blog", photo.FileName);
                var stream = new FileStream(path, FileMode.Create);
                photo.CopyToAsync(stream);

                blog.Photo = "/blog/" + photo.FileName;
            }
            currentBlogContent.Photo = blog.Photo;
            currentBlogContent.Title = blog.Title;
            currentBlogContent.PostedDate = blog.PostedDate;
            currentBlogContent.Description = blog.Description;
            currentBlogContent.Content = blog.Content;

            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index", "Blog", new { area = "admin" });
        }
    }
}