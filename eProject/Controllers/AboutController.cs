using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Data;
using eProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace eProject.Controllers
{
    [Route("About")]
    public class AboutController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AboutController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }


        public IActionResult Index()
        {
            List<About> data = _applicationDbContext.About.ToList();
            return View(data);
        }
    }
}