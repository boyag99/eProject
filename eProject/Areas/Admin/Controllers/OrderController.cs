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
    [Route("Admin/Order")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public OrderController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {

            List<User> users = await _applicationDbContext.Users.ToListAsync();

            List<Invoice> invoices = await _applicationDbContext.Invoices
                    .Include(i => i.OrderDetails)
                    .Include(i => i.ShippingAddress)
                    .Include(i => i.User)
                    .ToListAsync();

            foreach (var u in users)
            {
                foreach (var i in invoices)
                {
                    if (u.Id.Equals(i.BuyerId))
                    {
                        ViewBag.Buyer = u;
                    }
                }
            }

            return View(invoices);
        }

        [HttpGet]
        [Route("Invoice")]
        public async Task<IActionResult> Invoice(int id)
        {
            Invoice invoice = await _applicationDbContext.Invoices
                    .Include(i => i.OrderDetails)
                        .ThenInclude(od => od.Product)
                            .ThenInclude(p => p.Category)
                    .Include(i => i.ShippingAddress)
                    .Include(i => i.User)
                    .FirstOrDefaultAsync(i => i.Id == id);

            List<User> users = await _applicationDbContext.Users.Include(u => u.Address).ToListAsync();

            foreach (var u in users)
            {
                if (u.Id.Equals(invoice.BuyerId))
                {
                    ViewBag.Buyer = u;
                }
            }

            ViewBag.GeneralSetting = await _applicationDbContext.GeneralSettings.FirstOrDefaultAsync();
            ViewBag.WareHouseAddress = await _applicationDbContext.WareHouseAddresses.FirstOrDefaultAsync();
            ViewBag.Invoice = invoice;
            return View();
        }
    }
}
