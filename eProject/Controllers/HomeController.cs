using System;
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
using eProject.Service;
using System.Collections.Generic;

namespace eProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger,
            SignInManager<User> signInManager,
            ApplicationDbContext applicationDbContext,
            UserManager<User> userManager,
            IEmailSender emailSender)
        {
            _logger = logger;
            _signInManager = signInManager;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _emailSender = emailSender;
        }


        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.isHome = true;
            var featuredProducts = _applicationDbContext.Products.Include(p=>p.Photos).OrderByDescending(p => p.ProductId).Where(p => p.Status && p.Featured).Take(8).ToList();
            ViewBag.FeaturedProducts = featuredProducts;
            ViewBag.NewestProducts = _applicationDbContext.Products.Include(p => p.Photos).OrderByDescending(p => p.ProductId).Where(p => p.Status).Take(8).ToList();

            ViewBag.Post = _applicationDbContext.Blog.Include(b => b.User).Take(4).ToList();
            
            List<Product> saleProducts = _applicationDbContext.Products.Include(p => p.Photos).Where(p => p.SalePrice > 0).ToList();
            ViewBag.SaleProducts = saleProducts;
            return View();
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            LoginRequest loginRequest = new LoginRequest
            {
                ReturnUrl = returnUrl
            };
            return View(loginRequest);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

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

                return LocalRedirect(returnUrl);
            }

            if (signInResult.IsLockedOut)
            {
                var forgotPassLink = Url.Action(nameof(ForgotPassword), "Home", new { }, Request.Scheme);
                var content = string.Format("Your account is locked out, to reset your password, please click this link: {0}", forgotPassLink);

                var message = new EmailMessage(new string[] { loginRequest.Email }, "Locked out account information", content, null);
                await _emailSender.SendEmailAsync(message);

                ModelState.AddModelError("", "The account is locked out");
                return View();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(loginRequest);
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
                        Country = null
                    },
                    ProfileImage = null,
                };

                IdentityResult createUser =  await _userManager.CreateAsync(user, storeUserRequest.Password); // create new user

                if (createUser.Succeeded)
                {
                    IdentityResult addRoleUser = await _userManager.AddToRoleAsync(user, "Customer"); // add role for user

                    if (addRoleUser.Succeeded)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationLink = Url.Action(nameof(ConfirmEmail), "Home", new { token, email = user.Email }, Request.Scheme);

                        var message = new EmailMessage(new string[] { user.Email }, "Confirmation email link", confirmationLink, null);
                        await _emailSender.SendEmailAsync(message);

                        //await _signInManager.SignInAsync(user, isPersistent: false);

                        return RedirectToAction(nameof(SuccessRegistration));
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
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPassword forgotPasswordModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
                if (user == null)
                {
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callback = Url.Action(nameof(ResetPassword), "Home", new { token, email = user.Email }, Request.Scheme);

                var message = new EmailMessage(new string[] { user.Email }, "Reset password token", callback, null);
                await _emailSender.SendEmailAsync(message);

                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            return View(forgotPasswordModel);
        }

        [HttpGet]
        [Route("ForgotPasswordConfirmation")]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return View(model);
        }

        [HttpPost]
        [Route("ResetPassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPasswordModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
                if (user == null)
                {
                    RedirectToAction(nameof(ResetPasswordConfirmation));
                }

                var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
                if (!resetPassResult.Succeeded)
                {
                    foreach (var error in resetPassResult.Errors)
                    {
                        ModelState.TryAddModelError(error.Code, error.Description);
                    }

                    return View();
                }

                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }

            return View(resetPasswordModel);
        }

        [HttpGet]
        [Route("ResetPasswordConfirmation")]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return View("Error");
            }
                
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? nameof(ConfirmEmail) : "Error");
        }

        [HttpGet]
        [Route("SuccessRegistration")]
        public IActionResult SuccessRegistration()
        {
            return View();
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
