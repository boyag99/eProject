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
    [Route("Admin/General")]
    public class GeneralController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GeneralController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        
        // GET: /<controller>/
        public IActionResult Index()
        {
            GeneralSetting data = _applicationDbContext.GeneralSettings.FirstOrDefault(c => c.GeneralId == 1);
            return View(data);
        }
        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit(GeneralSetting general)
        {
            if (ModelState.IsValid)
            {
                GeneralSetting currentGeneral = await _applicationDbContext.GeneralSettings.FirstOrDefaultAsync(c => c.GeneralId == 1);
                currentGeneral.RegistrationArtistCost = general.RegistrationArtistCost;

                await _applicationDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(general);
        }
    }
}
