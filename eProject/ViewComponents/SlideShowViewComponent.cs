using eProject.Data;
using eProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProjectASP.ViewComponents
{
    [ViewComponent(Name = "SlideShow")]
    public class SlideShowViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SlideShowViewComponent(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<SlideShow> slideShows = _applicationDbContext.SlideShows.Where(o => o.Status).ToList();
            return View("Default", slideShows);
        }
    }
}
