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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/Product")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<User> _userManager;
        public ProductsController(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            ViewBag.Products = _applicationDbContext.Products
                .Include(p=>p.Photos)
                .Include(p => p.Category)
                .Include(p => p.User)
                .ToList();

            var data = ViewBag.Products;
            return View();
        }


        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var productViewModel = new ProductViewModel();
            productViewModel.Product = new Product();
            productViewModel.Categories = new List<SelectListItem>();
            var categories = _applicationDbContext.Categories.ToList();
            foreach (var category in categories)
            {
                var selectListItem = new SelectListItem
                {
                    Text = category.CategoryName,
                    Value = category.CategoryId.ToString(),
                };
               productViewModel.Categories.Add(selectListItem);      
            }
            return View("Create", productViewModel);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            User user = await _userManager.GetUserAsync(User);
            productViewModel.Product.UserId = user.Id;
            _applicationDbContext.Products.Add(productViewModel.Product);
            _applicationDbContext.SaveChanges();

            //Create default photo for new product
            var defaultPhoto = new Photo
            {
                Name = "no-image.jpg",
                Status = true,
                ProductId = productViewModel.Product.ProductId,
                Featured = true
            };
            _applicationDbContext.Photos.Add(defaultPhoto);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index", "Products", new { area = "Admin" });
        }


        [HttpGet]
        [Route("edit/{id}")]
        public ActionResult Edit(int id)
        {
            var productViewModel = new ProductViewModel();
            productViewModel.Product = _applicationDbContext.Products.Find(id);
            productViewModel.Categories = new List<SelectListItem>();
            var categories = _applicationDbContext.Categories.ToList();
            foreach (var category in categories)
            {
                var selectListItem = new SelectListItem
                {
                    Text = category.CategoryName,
                    Value = category.CategoryId.ToString(),
                };
                productViewModel.Categories.Add(selectListItem);
            }
            return View("Edit", productViewModel);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(int id, ProductViewModel productViewModel)
        {
            User user = await _userManager.GetUserAsync(User);
            productViewModel.Product.UserId = user.Id;
            _applicationDbContext.Entry(productViewModel.Product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index", "Products", new { area = "Admin" });
        }

        [HttpGet]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            Product product = _applicationDbContext.Products.SingleOrDefault(p => p.ProductId == id);
            if (product != null)
            {
                _applicationDbContext.Products.Remove(product);
                _applicationDbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}