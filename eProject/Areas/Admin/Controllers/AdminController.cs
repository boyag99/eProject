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
            User user = await _userManager.GetUserAsync(User);
            return View(user);
        }

        [HttpPost]
        [Route("Profile")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(string id, User user, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                User currentUser = await _applicationDbContext.Users
                   .FirstOrDefaultAsync(u => u.Id.Equals(id));
                var uniqueFileName = GeneralHelpers.UploadedFile(photo, USER_PATH).Result; // upload new image & return image name
                currentUser.FirstName = user.FirstName;
                currentUser.LastName = user.LastName;
                currentUser.PhoneNumber = user.PhoneNumber;
                currentUser.Gender = user.Gender;
                currentUser.DateOfBirthDay = user.DateOfBirthDay;
                currentUser.ProfileImage = uniqueFileName ?? currentUser.ProfileImage;

                _applicationDbContext.Users.Update(user);
                await _applicationDbContext.SaveChangesAsync();
            }

            return View("Profile");
        }
    }
}