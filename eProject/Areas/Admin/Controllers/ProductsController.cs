using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eProject.Models;
using Microsoft.AspNetCore.Authorization;
using eProject.Data;

namespace eProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/Product")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ProductsController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            List<Product> data = _applicationDbContext.Products.ToList();
            return View(data);
            
        }


        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
               
                _applicationDbContext.Products.Add(product);
                _applicationDbContext.SaveChanges();

                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }
            return View(product);
        }


        [HttpGet]
        [Route("Edit")]
        public IActionResult Edit(int id)
        {
            Product product = _applicationDbContext.Products.SingleOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {

            if (ModelState.IsValid)
            {
                Product currentProduct = _applicationDbContext.Products.SingleOrDefault(p => p.ProductId == id);

                if (currentProduct == null)
                {
                    return View(product);
                }

                currentProduct.Name = product.Name;
                currentProduct.Price = product.Price;
                currentProduct.SalePrice = product.SalePrice;
                currentProduct.Description = product.Description;
                currentProduct.Status = product.Status;
                

                _applicationDbContext.SaveChanges();

                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }
            return View(product);
        }

        [HttpGet]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            Product product = _applicationDbContext.Products.SingleOrDefault(p => p.ProductId == id);
            if ( product!= null)
            {
                _applicationDbContext.Products.Remove(product);
                _applicationDbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
        sdfds


    }
}