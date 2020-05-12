using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Data;
using eProject.Models;

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
            List<Category> data = _applicationDbContext.Categories.Where(c=>c.Status).ToList();
            return View(data);
        }


    }
}
