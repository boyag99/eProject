using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eProject.Models;
using Microsoft.AspNetCore.Authorization;
using eProject.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index()
        {
            List<Category> categories = await _applicationDbContext.Categories.Where(p=>p.Status==true).ToListAsync();
            return View(categories);
        }
        
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var category = new Category();
            return View("Create", category);
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _applicationDbContext.Categories.Add(category);
                await _applicationDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Create", category);
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            Category category = await _applicationDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if(category is null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View("Edit", category);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                Category categoryToUpdate = await _applicationDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
                categoryToUpdate.CategoryName = category.CategoryName;
                categoryToUpdate.Status = category.Status;
                await _applicationDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpGet]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Category category = await _applicationDbContext.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            _applicationDbContext.Categories.Remove(category);
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}