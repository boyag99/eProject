using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eProject.Models;
using Microsoft.AspNetCore.Authorization;
using eProject.Data;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace eProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/SlideShow")]
    public class SlideShowsController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHostEnvironment Environment;
        public SlideShowsController(ApplicationDbContext applicationDbContext, IHostEnvironment _Environment)
        {
            _applicationDbContext = applicationDbContext;
            Environment = _Environment;
        }


        [Route("")]
        [Route("Index")]

        public IActionResult Index()
        {
            List<SlideShow> data = _applicationDbContext.SlideShows.ToList();
            return View(data);
        }



        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var slideShow = new SlideShow();
            return View(slideShow);
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create(SlideShow slideShow, IFormFile photo)
        {
            if(ModelState.IsValid)
            {
                if(photo.Length >0)
                {
                    var path = Path.Combine(this.Environment.ContentRootPath, "wwwroot/slideshows", photo.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    photo.CopyToAsync(stream);
                    slideShow.Name = photo.FileName;

                    _applicationDbContext.SlideShows.Add(slideShow);
                    _applicationDbContext.SaveChanges();
                    return RedirectToAction("Index", "SlideShows", new { area = "admin" });
                }
               
            }

            return View();
        }




        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var slideShow = _applicationDbContext.SlideShows.Find(id);
                _applicationDbContext.SlideShows.Remove(slideShow);
                _applicationDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
            }
            return RedirectToAction("Index", "SlideShows", new { area = "admin" });
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
                var path = Path.Combine(this.Environment.ContentRootPath, "wwwroot/slideshows", photo.FileName);
                var stream = new FileStream(path, FileMode.Create);
                photo.CopyToAsync(stream);
                currentSlideShow.Name = photo.FileName;
            }
            currentSlideShow.Status = slideShow.Status;
            currentSlideShow.Title = slideShow.Title;
            currentSlideShow.Description = slideShow.Description;

            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index", "SlideShows", new { area = "admin" });
        }
    }
}