using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Data;
using eProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace eProject.Controllers
{
    [Route("Blog")]
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BlogController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }

        public IActionResult Index()
        {
            List<Blog> data = _applicationDbContext.Blog.ToList();
            return View(data);
        }

        [Route("ReadMore")]
        public IActionResult ReadMore(int id)
        {
            var data = _applicationDbContext.Blog.SingleOrDefault(b => b.BlogId.Equals(id));
            
            return View(data);
        }

    }
}