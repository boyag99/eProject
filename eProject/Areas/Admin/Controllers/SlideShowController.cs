using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eProject.Data;
using eProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace eProjectASP.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("admin")]
    [Route("admin/slideshow")]
    public class SlideShowController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHostEnvironment _environment;
        public SlideShowController(ApplicationDbContext applicationDbContext, IHostEnvironment environment)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;

        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.SlideShows = _applicationDbContext.SlideShows.ToList();
            return View();
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            var slideShow = new SlideShow();
            return View("Add", slideShow);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(SlideShow slideShow, IFormFile photo)
        {
            var path = Path.Combine(this._environment.ContentRootPath, "wwwroot/images/slideshows", photo.FileName);
            var stream = new FileStream(path, FileMode.Create);
            photo.CopyToAsync(stream);
            slideShow.Name = photo.FileName;
            _applicationDbContext.SlideShows.Add(slideShow);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("index", "slideshow", new { area = "admin" });
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
                var slideShow = _applicationDbContext.SlideShows.Find(id);
                _applicationDbContext.SlideShows.Remove(slideShow);
                _applicationDbContext.SaveChanges();
            return RedirectToAction("index", "slideshow", new { area = "admin" });
        }
        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var slideShow = _applicationDbContext.SlideShows.Find(id);
            return View("Edit", slideShow);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, SlideShow slideShow, IFormFile photo)
        {
            var currentSlideShow = _applicationDbContext.SlideShows.Find(id);
            if (photo != null && !string.IsNullOrEmpty(photo.FileName))
            {
                var path = Path.Combine(this._environment.ContentRootPath, "wwwroot/images/slideshows", photo.FileName);
                var stream = new FileStream(path, FileMode.Create);
                photo.CopyToAsync(stream);
                currentSlideShow.Name = photo.FileName;
            }
            currentSlideShow.Status = slideShow.Status;
            currentSlideShow.Title = slideShow.Title;
            currentSlideShow.Description = slideShow.Description;
            _applicationDbContext.SaveChanges();
            return RedirectToAction("index", "slideshow", new { area = "admin" });
        }
    }
}