using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using eProject.Models;
using eProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using eProject.Data;
using Microsoft.EntityFrameworkCore;

namespace eProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger, SignInManager<User> signInManager, ApplicationDbContext applicationDbContext, UserManager<User> userManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }


        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.isHome = true;
            var featuredProducts = _applicationDbContext.Products.Include(p=>p.Photos).OrderByDescending(p => p.ProductId).Where(p => p.Status && p.Featured).ToList();
            ViewBag.FeaturedProducts = featuredProducts;
            ViewBag.CountFeaturedProducts = featuredProducts.Count();
            return View();
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(loginRequest);
            }

            var signInResult = await _signInManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, loginRequest.RememberMe, lockoutOnFailure: true);

            if (signInResult.Succeeded)
            {
                if (User.IsInRole("ADMIN"))
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }

                return RedirectToAction("Index", "Account");
            }

            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "The account has been locked.");
                return View("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View("Index", loginRequest);
            }
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(StoreUserRequest storeUserRequest)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    FirstName = null,
                    LastName = null,
                    UserName = storeUserRequest.Email,
                    Email = storeUserRequest.Email,
                    PhoneNumber = storeUserRequest.PhoneNumber,
                    Gender = Models.User.GenderType.Other,
                    DateOfBirthDay = DateTime.Now,
                    Address = new Address
                    {
                        StreetAddress = null,
                        City = null,
                        County = null,
                        PostalCode = null,
                        State = null
                    },
                    ProfileImage = null,
                };

                IdentityResult createUser =  await _userManager.CreateAsync(user, storeUserRequest.Password); // create new user

                if (createUser.Succeeded)
                {
                    IdentityResult addRoleUser = await _userManager.AddToRoleAsync(user, "Customer"); // add role for user

                    if (addRoleUser.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        return RedirectToAction("Index", "Account");
                    }

                    foreach (var error in createUser.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                foreach (var error in createUser.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(storeUserRequest);
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
