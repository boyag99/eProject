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

namespace eProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<User> _signInManager;

        public HomeController(ILogger<HomeController> logger, SignInManager<User> signInManager, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _signInManager = signInManager;
            _applicationDbContext = applicationDbContext;
        }


        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.isHome = true;
            var featuredProducts = _applicationDbContext.Products.OrderByDescending(p => p.ProductId).Where(p => p.Status && p.Featured).ToList();
            ViewBag.FeaturedProducts = featuredProducts;
            ViewBag.CountFeaturedProducts = featuredProducts.Count();
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
                if (User.IsInRole("Admin") == true)
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
                return RedirectToAction(nameof(Index));
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
