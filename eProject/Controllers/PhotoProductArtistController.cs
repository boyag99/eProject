﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eProject.Data;
using eProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace eProjectASP.Areas.Admin.Controllers
{
    [Authorize(Roles = "Artist")]
    [Route("Artist/photo")]
    public class PhotoProductArtistController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHostEnvironment _environment;
        public PhotoProductArtistController(ApplicationDbContext applicationDbContext, IHostEnvironment environment)
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
            var path = Path.Combine(this._environment.ContentRootPath, "wwwroot/images/products", fileUpload.FileName);
            var stream = new FileStream(path, FileMode.Create);
            fileUpload.CopyToAsync(stream);
            photo.Name = fileUpload.FileName;
            _applicationDbContext.Photos.Add(photo);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("index", "PhotoProductArtist", new {id = productId });
        }
        [HttpGet]
        [Route("delete/{id}/productId")]
        public IActionResult Delete(int id, int productId)
        {
            var photo = _applicationDbContext.Photos.Find(id);
            _applicationDbContext.Photos.Remove(photo);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("index", "PhotoProductArtist", new {id = productId });
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
                var path = Path.Combine(this._environment.ContentRootPath, "wwwroot/images/products", fileUpload.FileName);
                var stream = new FileStream(path, FileMode.Create);
                fileUpload.CopyToAsync(stream);
                currentPhoto.Name = fileUpload.FileName;
            }
            if(photo.Status == true)
            {
                currentPhoto.Status = true;
            } else
            {
                currentPhoto.Status = false;
            }
            _applicationDbContext.SaveChanges();
            return RedirectToAction("index", "PhotoProductArtist", new {id = productId });
        }

        public IActionResult SetFeatured(int id, int productId)
        {
            var product = _applicationDbContext.Products.Include(p => p.Photos).FirstOrDefault(p => p.ProductId == productId);
            product.Photos.ToList().ForEach(p =>
            {
                p.Featured = false;
                _applicationDbContext.Photos.Update(p);

            });
            var photo = _applicationDbContext.Photos.FirstOrDefault(p => p.PhotoId == id);
            photo.Featured = true;

            _applicationDbContext.SaveChanges();
            return RedirectToAction("index", "PhotoProductArtist", new { id = productId });
        }
    }
}