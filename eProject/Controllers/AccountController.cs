﻿using System;
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
        private readonly SignInManager<User> _signInManager;

        public AccountController(ApplicationDbContext applicationDbContext , UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("AccountInformation")]
        public async Task<IActionResult> AccountInformation()
        {
            User user = await _userManager.GetUserAsync(User);

            UpdateUserRequest updateUserRequest = new UpdateUserRequest {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirthDay = user.DateOfBirthDay,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender
            };

            return View(updateUserRequest);
        }

        [HttpPost]
        [Route("UpdateAccount")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAccount(string id, UpdateUserRequest updateUserRequest)
        {
            if (ModelState.IsValid)
            {
                User user = await _applicationDbContext.Users
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

                _applicationDbContext.Users.Update(user);
                await _applicationDbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

           return View("Index", updateUserRequest);
        }

        [HttpGet]
        [Route("AddressBook")]
        public async Task<IActionResult> AddressBook()
        {
            User currentUser = await _userManager.GetUserAsync(User);
            User user = await _applicationDbContext.Users
                    .Include(u => u.Address)
                   .FirstOrDefaultAsync(u => u.Id.Equals(currentUser.Id));

            UpdateUserAddressRequest updateUserRequest = new UpdateUserAddressRequest();

            if (user.Address is null)
            {
                updateUserRequest.StreetAddress = null;
                updateUserRequest.PostalCode = null;
                updateUserRequest.City = null;
                updateUserRequest.County = null;
                updateUserRequest.State = null;
            } else
            {
                updateUserRequest.StreetAddress = user.Address.StreetAddress;
                updateUserRequest.PostalCode = user.Address.PostalCode;
                updateUserRequest.City = user.Address.City;
                updateUserRequest.County = user.Address.County;
                updateUserRequest.State = user.Address.State;
            }

            return View(updateUserRequest);
        }

        [HttpPost]
        [Route("UpdateAddressBook")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAddressBook(UpdateUserAddressRequest updateUserRequest)
        {
            if(ModelState.IsValid)
            {
                User user = await _userManager.GetUserAsync(User);

                if(user.Address is null)
                {
                    user.Address = new Address
                    {
                        StreetAddress = updateUserRequest.StreetAddress,
                        PostalCode = updateUserRequest.PostalCode,
                        City = updateUserRequest.City,
                        County = updateUserRequest.County,
                        State = updateUserRequest.State
                    };

                }
                else
                {
                    user.Address.StreetAddress = updateUserRequest.StreetAddress;
                    user.Address.PostalCode = updateUserRequest.PostalCode;
                    user.Address.City = updateUserRequest.City;
                    user.Address.County = updateUserRequest.County;
                    user.Address.State = updateUserRequest.State;
                }

                _applicationDbContext.Users.Update(user);
                await _applicationDbContext.SaveChangesAsync();

                return RedirectToAction(nameof(AddressBook));
            }

            return View("AddressBook", updateUserRequest);
        }

        [HttpGet]
        [Route("ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ChangePassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(UserResetPasswordRequest userResetPasswordRequest)
        {
            User user = await _userManager.GetUserAsync(User);

            if(user is null)
            {
                await _signInManager.SignOutAsync();

                RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                var removePassword = await _userManager.RemovePasswordAsync(user); // remove old password
                if (removePassword.Succeeded)
                {
                    IdentityResult addPassword = await _userManager.AddPasswordAsync(user, userResetPasswordRequest.Password); // update new password

                    if(addPassword.Succeeded)
                    {
                        await _signInManager.SignOutAsync();

                        return RedirectToAction("Login", "Home");
                    }

                    ModelState.AddModelError("", "Change password failed");
                }

                ModelState.AddModelError("", "Change password failed");
            }

            return View();
        }

        [HttpGet]
        [Route("Artist")]
        public IActionResult Artist()
        {
            return View();
        }
    }
}
