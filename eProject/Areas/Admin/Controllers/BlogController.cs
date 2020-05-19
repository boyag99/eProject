using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eProject.Models;
using Microsoft.AspNetCore.Authorization;
using eProject.Data;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace eProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/About")]
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        //private readonly IHostEnvironment Environment;
        public BlogController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            List<Blog> data = _applicationDbContext.Blog.ToList();
            return View(data);
        }
    }
}