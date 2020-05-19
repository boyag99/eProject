using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Data;
using eProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
  
            Product product = _applicationDbContext.Products.Include(p=>p.Photos).Include(p => p.Category)
                .Include(p => p.User).FirstOrDefault(p=>p.ProductId ==id);
            product.Hot += 1;
            _applicationDbContext.SaveChanges();

            List<Product> products = _applicationDbContext.Products.OrderByDescending(p => p.Hot).Take(1).ToList();
            ViewBag.Hot = false;
            foreach (var item in products)
            {
                if(item.ProductId == product.ProductId)
                {
                    if(product.Hot == 0)
                    {
                        ViewBag.Hot = false;
                    } else
                    {
                        ViewBag.Hot = true;
                    }
                }
            }

            var featuredPhoto = product.Photos.SingleOrDefault(p => p.Status && p.Featured);
            ViewBag.Product = product;
            ViewBag.FeaturedPhoto = featuredPhoto == null ? "no-image.jpg" : featuredPhoto.Name;
            ViewBag.ProductImages = product.Photos.Where(p => p.Status).ToList();
            ViewBag.RelatedProduct = _applicationDbContext.Products.Include(p => p.Photos).Where(p => p.CategoryId == product.CategoryId && p.ProductId != id && p.Status).ToList();
            return View("Details");
        }

        [Route("index")]
        public IActionResult Index()
        {
            var product = _applicationDbContext.Products.Include(p => p.Photos).Where(p => p.Status).ToList();
            ViewBag.Products = product;

            List<Product> hotProducts = _applicationDbContext.Products.Include(p => p.Photos).OrderByDescending(p => p.Hot).Take(3).ToList();
            ViewBag.Hot = hotProducts;

            List<Product> newProducts = _applicationDbContext.Products.Include(p => p.Photos).OrderByDescending(p => p.Created_At).Take(3).ToList();
            ViewBag.NewProducts = newProducts;

            List<Product> saleProducts = _applicationDbContext.Products.Include(p => p.Photos).Where(p=>p.SalePrice>0).Take(3).ToList();
            ViewBag.SaleProducts = saleProducts;


            return View("Index");
        }
        [Route("category/{id}")]
        public IActionResult Category(int id)
        {
            ViewBag.Category = _applicationDbContext.Categories.Include(c => c.Products).SingleOrDefault(p => p.CategoryId == id);

            ViewBag.Products = _applicationDbContext.Products
                .Include(p => p.Photos)
                .Where(p => p.CategoryId == id).ToList();

            ViewBag.CountProducts = _applicationDbContext.Products
                .Where(p => p.CategoryId == id)
                .Count(p => p.Status);

            return View("Category");
        }
    }
}