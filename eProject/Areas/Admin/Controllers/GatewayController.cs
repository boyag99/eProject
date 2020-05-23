using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eProject.Data;
using eProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/Gateway")]
    public class GatewayController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHostEnvironment _environment;
        public GatewayController(ApplicationDbContext applicationDbContext, IHostEnvironment environment)
        {
            _applicationDbContext = applicationDbContext;
            _environment = environment;
        }

        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            List<Gateway> gateways = await _applicationDbContext.Gateways.ToListAsync();


            return View(gateways);
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            var gateWay = new Gateway();
            return View("Add", gateWay);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(Gateway gateWay)
        {
            _applicationDbContext.Gateways.Add(gateWay);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("index", "gateway", new { area = "admin" });
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var gateway = _applicationDbContext.Gateways.Find(id);
            return View("Edit", gateway);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, Gateway gateway)
        {
            var currentGateway = _applicationDbContext.Gateways.Find(id);
            currentGateway.BankName = gateway.BankName;
            currentGateway.AccountNumber = gateway.AccountNumber;
            currentGateway.AccountName = gateway.AccountName;
            currentGateway.Branch = gateway.Branch;
            currentGateway.Message = gateway.Message;
            currentGateway.Status = gateway.Status;
            _applicationDbContext.SaveChanges();
            return RedirectToAction("index", "gateway", new { area = "admin" });
        }
    }
}
