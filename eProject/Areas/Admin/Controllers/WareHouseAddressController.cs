using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Data;
using eProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/WareHouseAddress")]
    public class WareHouseAddressController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public WareHouseAddressController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            WareHouseAddress ware = _applicationDbContext.WareHouseAddresses.FirstOrDefault(w => w.WareHouseId == 1);
           
            return View(ware);
        }
        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit(WareHouseAddress ware)
        {
            if (ModelState.IsValid)
            {
                WareHouseAddress wareHouse = await _applicationDbContext.WareHouseAddresses.FirstOrDefaultAsync(c => c.WareHouseId == 1);
                wareHouse.CompanyName = ware.CompanyName;
                wareHouse.StreetAddress = ware.StreetAddress;
                wareHouse.PostalCode = ware.PostalCode;
                wareHouse.PhoneNumber = ware.PhoneNumber;
                wareHouse.Email = ware.Email;


                await _applicationDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ware);
        }
    }
}
    
