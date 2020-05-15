using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Data;
using eProject.Models;
using Microsoft.EntityFrameworkCore;

namespace eProject.ViewComponents
{
    [ViewComponent(Name = "Category")]
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CategoryViewComponent(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _applicationDbContext.Categories.Where(o => o.Status).ToListAsync());
            //return View(await _applicationDbContext.Categories.Where(c => c.Parent == null).Where(o => o.Status).ToListAsync());
        }


    }
}
