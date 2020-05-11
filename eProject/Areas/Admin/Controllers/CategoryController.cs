using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eProject.Models;
using Microsoft.AspNetCore.Authorization;
using eProject.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace eProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/Category")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CategoryController (ApplicationDbContext applicationDbContext) 
        {
            _applicationDbContext = applicationDbContext;
        }


        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            List<Category> data = _applicationDbContext.Categories.ToList();
            return View(data);
        }


        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")] 
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category cat)
        {
            if(ModelState.IsValid)
            {
                _applicationDbContext.Categories.Add(cat);
                _applicationDbContext.SaveChanges();

                return RedirectToAction("Index", "Category", new {area = "Admin" });
            }
            return View(cat);
        }



        [HttpGet]
        [Route("Edit")]
        public IActionResult Edit(int id)
        {
            Category cat = _applicationDbContext.Categories.SingleOrDefault(c => c.Id == id);
            if(cat==null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(cat);
        }

        [HttpPost]
        [Route("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Category category)
        {

            if (ModelState.IsValid)
            {
                Category currentCategory = _applicationDbContext.Categories.SingleOrDefault(c => c.Id == id);

                if(currentCategory==null)
                {
                    return View(category);
                }

                currentCategory.Name = category.Name;
                currentCategory.Status = category.Status;

                _applicationDbContext.SaveChanges();

                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View(category);
        }

        [HttpGet]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            Category cat = _applicationDbContext.Categories.SingleOrDefault(c => c.Id == id);
            if (cat != null)
            {
                _applicationDbContext.Categories.Remove(cat);
                _applicationDbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        [Route("AddSubCategory")]
        public IActionResult AddSubCategory(int id)
        {
            var subCat = new Category()
            {
                ParentId = id
            };
            return View(subCat);


            //Category cat = _applicationDbContext.Categories.SingleOrDefault(c => c.Id == id);
            //if (cat == null)
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(cat);
        }

        [HttpPost]
        [Route("AddSubCategory")]
        [ValidateAntiForgeryToken]
        public IActionResult AddSubCategory(Category subCat)
        {

            if (ModelState.IsValid)
            {
                _applicationDbContext.Categories.Add(subCat);
                _applicationDbContext.SaveChanges();

                return RedirectToAction("Index", "Category", new { area = "Admin" });
            }
            return View(subCat);
        }

        

    }
}