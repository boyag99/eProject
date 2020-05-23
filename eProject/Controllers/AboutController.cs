using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Data;
using eProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [Route("")]
        [Route("Index")]
            public async Task<IActionResult> Index()
            {
                List<About> data = await _applicationDbContext.About.ToListAsync();
                return View(data);
            }
        }
    }