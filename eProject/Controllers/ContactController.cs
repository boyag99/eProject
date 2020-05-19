using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Data;
using eProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace eProject.Controllers
{
    [Route("Contact")]
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ContactController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }

        public IActionResult Index()
        {
            Contact data = _applicationDbContext.Contacts.FirstOrDefault(c => c.Id == 1);
            return View(data);

        }
    }
}