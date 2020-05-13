using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eProject.Models;
using Microsoft.AspNetCore.Authorization;
using eProject.Data;
using eProject.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

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
           var productViewModel = new ProductViewModel();
            productViewModel.Product = new Product();
            productViewModel.Categories = new List<SelectListItem>();
            var categories = _applicationDbContext.Categories.ToList();
            foreach(var category in categories)
            {
                var group = new SelectListGroup { Name = category.Name };
                if(category.InverseParent !=null && category.InverseParent.Count > 0)
                {
                    foreach (var subCategory in category.InverseParent)
                    {
                        var selectListItem = new SelectListItem
                        {
                            Text = subCategory.Name,
                            Value = subCategory.Id.ToString(),
                            Group = group
                        };
                        productViewModel.Categories.Add(selectListItem);
                    }
                }
            }
            return View(productViewModel);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                _applicationDbContext.Products.Add(productViewModel.Product);
                _applicationDbContext.SaveChanges();


                //Create default photo for new product
                //var defaultPhoto = new Photo
                //{
                //    Name = "photo1.jpg",
                //    Status = true,
                //    ProductId = productViewModel.Product.ProductId,
                //    Featured = true
                //};

                //_applicationDbContext.Photos.Add(defaultPhoto);
                //_applicationDbContext.SaveChanges();

                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }
            return View(productViewModel);
        }


        [HttpGet]
        [Route("Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var productViewModel = new ProductViewModel();
            productViewModel.Product = _applicationDbContext.Products.Find(id);
            productViewModel.Categories = new List<SelectListItem>();
            var categories = _applicationDbContext.Categories.ToList();
            foreach (var category in categories)
            {
                var group = new SelectListGroup { Name = category.Name };
                if (category.InverseParent != null && category.InverseParent.Count > 0)
                {
                    foreach (var subCategory in category.InverseParent)
                    {
                        var selectListItem = new SelectListItem
                        {
                            Text = subCategory.Name,
                            Value = subCategory.Id.ToString(),
                            Group = group
                        };
                        productViewModel.Categories.Add(selectListItem);
                    }
                }
            }
            return View("Edit", productViewModel);
        }




        [HttpPost]
        [Route("Edit")]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            _applicationDbContext.Entry(productViewModel.Product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index", "Products", new { area = "admin" });
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


      

    }
}