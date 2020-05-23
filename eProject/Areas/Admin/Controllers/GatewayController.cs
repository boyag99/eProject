using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Data;
using eProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/Gateway")]
    public class GatewayController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GatewayController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            List<Gateway> gateways = await _applicationDbContext.Gateways.ToListAsync();


            return View(gateways);
        }


    }
}
