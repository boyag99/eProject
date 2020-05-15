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
    [Route("admin/photo")]
    public class PhotoController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHostEnvironment _environment;
        public PhotoController(ApplicationDbContext applicationDbContext, IHostEnvironment environment)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
        }
        [Route("index/{id}")]
        public IActionResult Index(int id)
        {
            ViewBag.Product = _applicationDbContext.Products.Find(id);
            ViewBag.Photos = _applicationDbContext.Photos.Where(p => p.ProductId == id).ToList();
            return View();
        }

        [HttpGet]
        [Route("add/{id}")]
        public IActionResult Add(int id)
        {
            ViewBag.Product = _applicationDbContext.Products.Find(id);
            var photo = new Photo
            {
                ProductId = id
            };
            return View("Add", photo);
        }

        [HttpPost]
        [Route("add/{productId}")]
        public IActionResult Add(int productId, Photo photo, IFormFile fileUpload)
        {
            var path = Path.Combine(this._environment.ContentRootPath, "wwwroot/products", fileUpload.FileName);
            var stream = new FileStream(path, FileMode.Create);
            fileUpload.CopyToAsync(stream);
            photo.Name = fileUpload.FileName;
            _applicationDbContext.Photos.Add(photo);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("index", "photo", new { area = "admin", id = productId });
        }
        [HttpGet]
        [Route("delete/{id}/productId")]
        public IActionResult Delete(int id, int productId)
        {
            var photo = _applicationDbContext.Photos.Find(id);
            _applicationDbContext.Photos.Remove(photo);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("index", "photo", new { area = "admin", id = productId });
        }
        [HttpGet]
        [Route("edit/{id}/{productId}")]
        public IActionResult Edit(int id, int productId)
        {
            ViewBag.Product = _applicationDbContext.Products.Find(productId);
            var photo = _applicationDbContext.Photos.Find(id);
            return View("Edit", photo);
        }

        [HttpPost]
        [Route("edit/{id}/{productId}")]
        public IActionResult Edit(int productId, Photo photo, IFormFile fileUpload)
        {
            var currentPhoto = _applicationDbContext.Photos.Find(photo.PhotoId);
            if (fileUpload != null && !string.IsNullOrEmpty(fileUpload.FileName))
            {
                var path = Path.Combine(this._environment.ContentRootPath, "wwwroot/products", fileUpload.FileName);
                var stream = new FileStream(path, FileMode.Create);
                fileUpload.CopyToAsync(stream);
                currentPhoto.Name = fileUpload.FileName;
            }
            currentPhoto.Status = photo.Status;
            _applicationDbContext.SaveChanges();
            return RedirectToAction("index", "photo", new { area = "admin", id = productId });
        }

        [HttpGet]
        [Route("SetNonFeatured/{id}/{productId}")]
        public IActionResult SetNonFeatured(int id, int productId)
        {
            var photo = _applicationDbContext.Photos.Find(id);
            photo.Featured = false;
            _applicationDbContext.SaveChanges();

            return RedirectToAction("index", "photo", new { area = "admin", id = productId });
        }
        [HttpGet]
        [Route("SetFeatured/{id}/{productId}")]
        public IActionResult SetFeatured(int id, int productId)
        {
            var photo = _applicationDbContext.Photos.Find(id);
            photo.Featured = true;
            _applicationDbContext.SaveChanges();
            return RedirectToAction("index", "photo", new { area = "admin", id = productId });
        }
    }
}