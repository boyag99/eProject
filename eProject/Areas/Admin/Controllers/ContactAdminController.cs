using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Data;
using eProject.Models;
using eProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/ContactAdmin")]
    public class ContactAdminController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ContactAdminController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            List<ContactAdmin> contactAdmins = await _applicationDbContext.ContactAdmins.Include(ca => ca.User).ToListAsync();

            return View(contactAdmins);
        }

        [Route("Details")]
        public async Task<IActionResult> Details(int id)
        {
            ContactAdmin contactAdmin = await _applicationDbContext.ContactAdmins.Include(ca => ca.User).FirstOrDefaultAsync(p => p.ContactAdminId == id);

            if(contactAdmin is null)
            {
                return RedirectToAction(nameof(Index));
            }

            UpdateContactAdminRequest updateContactAdminRequest = new UpdateContactAdminRequest
            {
                ContactAdminId = contactAdmin.ContactAdminId,
                Message = contactAdmin.Message,
                FirstName = contactAdmin.User.FirstName,
                LastName = contactAdmin.User.LastName,
                Status = contactAdmin.Status
            };

            return View(updateContactAdminRequest);
        }

        [Route("Approved")]
        [HttpPost]
        public async Task<IActionResult> Approved(UpdateContactAdminRequest updateContactAdminRequest)
        {
            ContactAdmin contactAdmin = await _applicationDbContext.ContactAdmins.FirstOrDefaultAsync(ca => ca.ContactAdminId == updateContactAdminRequest.ContactAdminId);

            if(contactAdmin is null)
            {
                return RedirectToAction(nameof(Details), new { id = updateContactAdminRequest.ContactAdminId });
            }

            contactAdmin.Status = updateContactAdminRequest.Status;
            contactAdmin.Updated_At = DateTime.Now;

            await _applicationDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = updateContactAdminRequest.ContactAdminId });
        }
    }
}
