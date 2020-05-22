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
using Microsoft.EntityFrameworkCore;

namespace eProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/Contact")]

    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
       
        public ContactController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            Contact data = _applicationDbContext.Contacts.FirstOrDefault(c => c.Id == 1);
            return View(data);
        }


        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                Contact currentContactContent = await _applicationDbContext.Contacts.FirstOrDefaultAsync(c => c.Id == 1);
                currentContactContent.Address = contact.Address;
                currentContactContent.Telephone = contact.Telephone;
                currentContactContent.Fax = contact.Fax;
                currentContactContent.Email = contact.Email;

                await _applicationDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }
    }
}