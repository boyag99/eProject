using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Data;
using Microsoft.AspNetCore.Mvc;

namespace eProject.Controllers
{
    [Route("product")]
    public class ProductUserController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductUserController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }
        [Route("details/{id}")]
        public IActionResult Details(int id)
        {
            var product = _applicationDbContext.Products.Find(id);
            var featuredPhoto = product.Photos.SingleOrDefault(p => p.Status && p.Featured);
            ViewBag.Product = product;
            ViewBag.FeaturedPhoto = featuredPhoto == null ? "no-image.jpg" : featuredPhoto.Name;
            ViewBag.ProductImages = product.Photos.Where(p => p.Status).ToList();
            ViewBag.RelatedProduct = _applicationDbContext.Products.Where(p => p.CategoryId == product.CategoryId && p.ProductId != id && p.Status).ToList();
            return View("Details");
        }

        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.Products = _applicationDbContext.Products.Where(p => p.Status).ToList();
            return View("Index");
        }
        [Route("category/{id}")]
        public IActionResult Category(int id)
        {
            var category = _applicationDbContext.Categories.Find(id);
            ViewBag.Category = category;
            ViewBag.CountProducts = category.Products.Count(p => p.Status);
            ViewBag.Products = category.Products.Where(p => p.Status).ToList();
            return View("Category");
        }
    }
}