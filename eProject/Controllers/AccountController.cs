using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.App.Helpers;
using eProject.Data;
using eProject.Models;
using eProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eProject.Controllers
{
    [Authorize]
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<User> _userManager;

        public AccountController(ApplicationDbContext applicationDbContext , UserManager<User> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            User user = await _userManager.GetUserAsync(User);

            UpdateUserRequest updateUserRequest = new UpdateUserRequest();

            if(user.Address is null)
            {
                updateUserRequest.Id = user.Id;
                updateUserRequest.Email = user.Email;
                updateUserRequest.FirstName = user.FirstName;
                updateUserRequest.LastName = user.LastName;
                updateUserRequest.DateOfBirthDay = user.DateOfBirthDay;
                updateUserRequest.PhoneNumber = user.PhoneNumber;
                updateUserRequest.Gender = user.Gender;
            } else
            {
                updateUserRequest.Id = user.Id;
                updateUserRequest.Email = user.Email;
                updateUserRequest.FirstName = user.FirstName;
                updateUserRequest.LastName = user.LastName;
                updateUserRequest.DateOfBirthDay = user.DateOfBirthDay;
                updateUserRequest.PhoneNumber = user.PhoneNumber;
                updateUserRequest.Gender = user.Gender;
                updateUserRequest.PostalCode = user.Address.PostalCode;
                updateUserRequest.StreetAddress = user.Address.StreetAddress;
                updateUserRequest.County = user.Address.County;
                updateUserRequest.City = user.Address.City;
                updateUserRequest.State = user.Address.State;
            }

            return View(updateUserRequest);
        }

        [HttpPost]
        [Route("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string id, UpdateUserRequest updateUserRequest)
        {
            if (ModelState.IsValid)
            {
                User user = await _applicationDbContext.Users
                   .Include(u => u.Address)
                   .FirstOrDefaultAsync(u => u.Id.Equals(id));

                if (user is null)
                {
                    return RedirectToAction(nameof(Index));
                }

                user.FirstName = updateUserRequest.FirstName;
                user.LastName = updateUserRequest.LastName;
                user.PhoneNumber = updateUserRequest.PhoneNumber;
                user.Gender = updateUserRequest.Gender;
                user.DateOfBirthDay = updateUserRequest.DateOfBirthDay;
                user.Address.StreetAddress = updateUserRequest.StreetAddress;
                user.Address.PostalCode = updateUserRequest.PostalCode;
                user.Address.City = updateUserRequest.City;
                user.Address.County = updateUserRequest.County;
                user.Address.State = updateUserRequest.State;

                _applicationDbContext.Users.Update(user);
                await _applicationDbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

           return View("Index", updateUserRequest);
        }
    }
}
