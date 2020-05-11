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
    }
}