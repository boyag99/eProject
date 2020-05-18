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

namespace eProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/About")]
    public class AboutController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        //private readonly IHostEnvironment Environment;
        public AboutController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            

        }

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            List<About> data = _applicationDbContext.About.ToList();
            return View(data);
        }


        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var about  = new About();
            return View("Create", about);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(About about, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                if (photo.Length > 0)
                {
                    var path = Path.Combine("wwwroot/about", photo.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    photo.CopyToAsync(stream);

                    about.Image = "/about/"+photo.FileName;

                    _applicationDbContext.About.Add(about);
                    _applicationDbContext.SaveChanges();
                    return RedirectToAction("Index", "About", new { area = "admin" });
                }

            }

            return View();
        }


        

      

        [HttpGet]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {

            About about =  _applicationDbContext.About.SingleOrDefault(a => a.AboutId == id);

            _applicationDbContext.About.Remove(about);
            _applicationDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var about = _applicationDbContext.About.Find(id);
            return View("Edit", about);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, About about, IFormFile photo)
        {
            var currentAboutContent = _applicationDbContext.About.Find(id);
            if (photo != null && !string.IsNullOrEmpty(photo.FileName))
            {
                var path = Path.Combine( "wwwroot/about", photo.FileName);
                var stream = new FileStream(path, FileMode.Create);
                photo.CopyToAsync(stream);

                about.Image = "/about/" + photo.FileName;
            }
            currentAboutContent.Image = about.Image;
            currentAboutContent.Name = about.Name;
            currentAboutContent.Description = about.Description;
            currentAboutContent.Slogan = about.Slogan;

            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index", "About", new { area = "admin" });
        }
    }
}