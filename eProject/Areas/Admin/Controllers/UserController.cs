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

namespace eProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/User")]
    public class UserController : Controller
    {
        private const string USER_PATH = "images/users";

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(ApplicationDbContext applicationDbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            List<User> users = await _applicationDbContext.Users
                                    .Include(u => u.Address)
                                    .ToListAsync();
            List<UserInfo> userInfos = new List<UserInfo>();

            foreach (var user in users)
            {
                userInfos.Add(new UserInfo
                {
                    UserId = user.Id,
                    ProfileImage = user.ProfileImage,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = await _userManager.GetRolesAsync(user),
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    Gender = user.Gender,
                    DateOfBirthDay = user.DateOfBirthDay,
                    StreetAddress = user.Address.StreetAddress,
                    County = user.Address.County,
                    City = user.Address.City,
                    Country = user.Address.Country,
                    PostalCode = user.Address.PostalCode
                });
            }

            return View("Index", userInfos);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(StoreUserRequest storeUserRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(storeUserRequest);
            }

            var uniqueFileName = GeneralHelpers.UploadedFile(storeUserRequest.ProfileImage, USER_PATH).Result; // upload new image & return image name

            User user = new User
            {
                FirstName = storeUserRequest.FirstName,
                LastName = storeUserRequest.LastName,
                UserName = storeUserRequest.Email,
                Email = storeUserRequest.Email,
                PhoneNumber = storeUserRequest.PhoneNumber,
                Gender = storeUserRequest.Gender,
                DateOfBirthDay = Convert.ToDateTime(storeUserRequest.DateOfBirthDay),
                Address = new Address {
                    StreetAddress = storeUserRequest.StreetAddress,
                    City = storeUserRequest.City,
                    County = storeUserRequest.County,
                    PostalCode = storeUserRequest.PostalCode,
                    Country = storeUserRequest.Country
                },
                ProfileImage = uniqueFileName
            };

            await _userManager.CreateAsync(user, storeUserRequest.Password); // create new user

            await RoleExistsAsync(storeUserRequest.Role); // Check exists role & create role

            IdentityResult result = await _userManager.AddToRoleAsync(user, storeUserRequest.Role); // add role for user

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(storeUserRequest);
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id is null)
            {
                return RedirectToAction(nameof(Index));
            }

            User user = await _applicationDbContext.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Id.Equals(id));

            if(user is null)
            {
                return RedirectToAction(nameof(Index));
            }

            UpdateUserRequest updateUserRequest = new UpdateUserRequest
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = await _userManager.GetRolesAsync(user),
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Gender = user.Gender,
                DateOfBirthDay = user.DateOfBirthDay,
                PostalCode = user.Address.PostalCode,
                StreetAddress = user.Address.StreetAddress,
                County = user.Address.County,
                City = user.Address.City,
                Country = user.Address.Country

            };

            return View(updateUserRequest);
        }

       [HttpPost]
       [Route("Edit")]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UpdateUserRequest updateUserRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(updateUserRequest);
            }

            User user = await _applicationDbContext.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Id.Equals(id));

            if (user is null)
            {
                return RedirectToAction(nameof(Index));
            }

            var userImage = user.ProfileImage;
            var uniqueImageName = GeneralHelpers.UploadedFile(updateUserRequest.ProfileImage, USER_PATH).Result; // upload image & return image name

            user.FirstName = updateUserRequest.FirstName;
            user.LastName = updateUserRequest.LastName;
            user.Email = updateUserRequest.Email;
            user.PhoneNumber = updateUserRequest.PhoneNumber;
            user.Gender = updateUserRequest.Gender;
            user.DateOfBirthDay = updateUserRequest.DateOfBirthDay;
            user.Address.StreetAddress = updateUserRequest.StreetAddress;
            user.Address.PostalCode = updateUserRequest.PostalCode;
            user.Address.City = updateUserRequest.City;
            user.Address.County = updateUserRequest.County;
            user.Address.Country = updateUserRequest.Country;
            user.ProfileImage = uniqueImageName ?? userImage;

            _applicationDbContext.Users.Update(user);
            await _applicationDbContext.SaveChangesAsync();

            if (!(uniqueImageName is null))
            {
                GeneralHelpers.DeleteFile(USER_PATH, userImage); // if update success then delete path old image
            }


            await UpdatePassword(user, updateUserRequest.Password); // update password if not null

            await UpdateRole(user, updateUserRequest.Role[0]); // update user new role

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        [Route("Lock")]
        public async Task<IActionResult> Lock(string id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            User user = await _applicationDbContext.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Id.Equals(id));

            if (user is null)
            {
                return NotFound();
            }

            user.LockoutEnd = DateTime.Now.AddYears(1);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("Unlock")]
        public async Task<IActionResult> Unlock(string id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            User user = await _applicationDbContext.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Id.Equals(id));

            if (user is null)
            {
                return NotFound();
            }

            user.LockoutEnd = DateTime.Now;
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("Deposit")]
        public IActionResult Deposit()
        {
            return View();
        }

        [HttpPost]
        [Route("Deposit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deposit(DepositUserRequest depositUserRequest)
        {
            User user = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Email.Equals(depositUserRequest.Email));

            if(user is null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                user.Balance += depositUserRequest.Balance;

                await _applicationDbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
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



        private async Task UpdateRole(User user, string role)
        {
            var roleUser = await _userManager.GetRolesAsync(user); // get role user
            var removeRole = await _userManager.RemoveFromRoleAsync(user, roleUser[0]); // remove user old role

            if (removeRole.Succeeded)
            {
                await RoleExistsAsync(role); // check exists role & create role

                await _userManager.AddToRoleAsync(user, role); // add role for user
            }
        }

        private async Task RoleExistsAsync(string role)
        {
            if (!await _roleManager.RoleExistsAsync(role)) // check exist role
            {
                await _roleManager.CreateAsync(new IdentityRole(role)); // if role not exist then create new role
            }
        }

    }

    
}
