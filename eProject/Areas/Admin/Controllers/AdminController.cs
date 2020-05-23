using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.App.Helpers;
using eProject.Data;
using eProject.Models;
using eProject.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eProject.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    public class AdminController : Controller
    {
        private const string USER_PATH = "images/users";
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<User> _userManager;

        public AdminController(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("Profile")]
        public async Task<IActionResult> Profile()
        {
            List<User> users = await _applicationDbContext.Users.Include(u => u.Address).ToListAsync();
            User currentUser = await _userManager.GetUserAsync(User);
            UpdateUserRequest updateUserRequest = new UpdateUserRequest();
 
            if (currentUser is null)
            {
                return RedirectToAction(nameof(Profile));
            }

            foreach (var user in users)
            {
                if (user.Id.Equals(currentUser.Id))
                {
                    updateUserRequest.Id = user.Id;
                    updateUserRequest.FirstName = user.FirstName;
                    updateUserRequest.LastName = user.LastName;
                    updateUserRequest.PhoneNumber = user.PhoneNumber;
                    updateUserRequest.Email = user.Email;
                    updateUserRequest.Gender = user.Gender;
                    updateUserRequest.DateOfBirthDay = user.DateOfBirthDay;
                    updateUserRequest.PostalCode = user.Address.PostalCode;
                    updateUserRequest.StreetAddress = user.Address.StreetAddress;
                    updateUserRequest.County = user.Address.County;
                    updateUserRequest.City = user.Address.City;
                    updateUserRequest.Country = user.Address.Country;
                }
            }

            return View(updateUserRequest);
        }

        [HttpPost]
        [Route("Profile")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(UpdateUserRequest updateUserRequest)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            List<User> users = await _applicationDbContext.Users.Include(u => u.Address).ToListAsync();
            User currentUser = await _userManager.GetUserAsync(User);

            if (currentUser is null)
            {
                return RedirectToAction(nameof(Profile));
            }

            var userImage = currentUser.ProfileImage;
            var uniqueImageName = GeneralHelpers.UploadedFile(updateUserRequest.ProfileImage, USER_PATH).Result; // upload image & return image name

            foreach (User user in users)
            {
                if (user.Id.Equals(currentUser.Id))
                {
                    currentUser.FirstName = updateUserRequest.FirstName;
                    currentUser.LastName = updateUserRequest.LastName;
                    currentUser.Email = updateUserRequest.Email;
                    currentUser.PhoneNumber = updateUserRequest.PhoneNumber;
                    currentUser.Gender = updateUserRequest.Gender;
                    currentUser.DateOfBirthDay = updateUserRequest.DateOfBirthDay;
                    currentUser.Address.StreetAddress = updateUserRequest.StreetAddress;
                    currentUser.Address.PostalCode = updateUserRequest.PostalCode;
                    currentUser.Address.City = updateUserRequest.City;
                    currentUser.Address.County = updateUserRequest.County;
                    currentUser.Address.Country = updateUserRequest.Country;
                    currentUser.ProfileImage = uniqueImageName ?? userImage;
                }
            }

            _applicationDbContext.Users.Update(currentUser);
            await _applicationDbContext.SaveChangesAsync();


            await UpdatePassword(currentUser, updateUserRequest.Password); // update password if not null


            return RedirectToAction(nameof(Profile));
        }

        private async Task UpdatePassword(User user, string password)
        {
            if (password != null)
            {
                var removePassword = await _userManager.RemovePasswordAsync(user); // remove old password
                if (removePassword.Succeeded)
                {
                    await _userManager.AddPasswordAsync(user, password); // update new password
                }
            }
        }
    }
}