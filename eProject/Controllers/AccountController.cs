using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.App.Helpers;
using eProject.Data;
using eProject.Models;
using eProject.Models.ViewModels;
using eProject.Service;
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
        private const string USER_PATH = "images/users";
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(ApplicationDbContext applicationDbContext , UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
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

                var uniqueFileName = GeneralHelpers.UploadedFile(updateUserRequest.ProfileImage, USER_PATH).Result; // upload new image & return image name


                user.FirstName = updateUserRequest.FirstName;
                user.LastName = updateUserRequest.LastName;
                user.PhoneNumber = updateUserRequest.PhoneNumber;
                user.Gender = updateUserRequest.Gender;
                user.DateOfBirthDay = updateUserRequest.DateOfBirthDay;
                user.ProfileImage = uniqueFileName;

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
                updateUserRequest.Country = null;
            } else
            {
                updateUserRequest.StreetAddress = user.Address.StreetAddress;
                updateUserRequest.PostalCode = user.Address.PostalCode;
                updateUserRequest.City = user.Address.City;
                updateUserRequest.County = user.Address.County;
                updateUserRequest.Country = user.Address.Country;
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
                        Country = updateUserRequest.Country
                    };

                }
                else
                {
                    user.Address.StreetAddress = updateUserRequest.StreetAddress;
                    user.Address.PostalCode = updateUserRequest.PostalCode;
                    user.Address.City = updateUserRequest.City;
                    user.Address.County = updateUserRequest.County;
                    user.Address.Country = updateUserRequest.Country;
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
        public async Task<IActionResult> Artist()
        {
            User user = await _userManager.GetUserAsync(User);
            List<User> users = await _applicationDbContext.Users.ToListAsync();
            GeneralSetting generalSetting = await _applicationDbContext.GeneralSettings.FirstOrDefaultAsync();
            //List<OrderDetail> orderDetails = await _applicationDbContext.InvoiceDetails
            //                                            .Include(id => id.Product)
            //                                            .Include(id => id.Invoice)
            //                                            .Where(id => id.Invoice.SellerId == user.Id)
            //                                            .ToListAsync();

            List<Invoice> invoices = await _applicationDbContext.Invoices
                .Include(i => i.OrderDetails)
                .Include(i => i.ShippingAddress)
                .Include(i => i.User)
                .Where(i => i.SellerId == user.Id)
                .ToListAsync();

            ViewBag.User = user;
            ViewBag.GeneralSetting = generalSetting;
            ViewBag.Invoices = invoices;

            foreach (var u in users)
            {
                foreach (var i in invoices)
                {
                    if (u.Id.Equals(i.BuyerId))
                    {
                        ViewBag.Buyer = u;
                    }
                }
            }



            return View();
        }

        [HttpPost]
        [Route("RegisterArtist")]
        public async Task<IActionResult> RegisterArtist(ArtistRegisterRequest artistRegisterRequest)
        {
            GeneralSetting generalSetting = await _applicationDbContext.GeneralSettings.FirstOrDefaultAsync();
            User user = await _userManager.GetUserAsync(User);

            if(user.Balance < generalSetting.RegistrationArtistCost)
            {

                ViewData["Message"] = "Balance is not enough. Please refill.";
                return RedirectToAction(nameof(Artist));
            }

            var userImage = user.ProfileImage;
            var uniqueImageName = GeneralHelpers.UploadedFile(artistRegisterRequest.ProfileImage, USER_PATH).Result; // upload image & return image name

            if (ModelState.IsValid)
            {
                var costCharge = user.Balance - generalSetting.RegistrationArtistCost;

                user.Exhibition = artistRegisterRequest.Exhibition;
                user.Biography = artistRegisterRequest.Biography;
                user.ProfileImage = uniqueImageName ?? userImage;
                user.Balance = costCharge;

                _applicationDbContext.Users.Update(user);
                await _applicationDbContext.SaveChangesAsync();

                await UpdateRole(user, "Artist");

                await _signInManager.RefreshSignInAsync(user);
                return RedirectToAction("Artist", "Account");
            }

            ViewData["Message"] = "Error";
            return RedirectToAction(nameof(Artist));
        }

        [HttpGet]
        [Route("ContactAdmin")]
        public IActionResult ContactAdmin()
        {
            return View();
        }

        [HttpPost]
        [Route("ContactAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactAdmin(ContactAdminRequest contactAdminRequest)
        {
            User user = await _userManager.GetUserAsync(User);

            if(user is null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                ContactAdmin contactAdmin = new ContactAdmin
                {
                    Message = contactAdminRequest.Message,
                    UserId = user.Id
                };

                _applicationDbContext.ContactAdmins.Add(contactAdmin);
                await _applicationDbContext.SaveChangesAsync();

                var contentUser = string.Format("Hello {0} {1}! Thank you for emailing us. We will review and respond as soon as possible.", user.FirstName, user.LastName);
                var messageUser = new EmailMessage(new string[] { user.Email }, "Contact Admin", contentUser, null);

                List<string> emailAdmin = new List<string>();
                List<User> users = await _applicationDbContext.Users.ToListAsync();
                foreach (var i in users)
                {
                    IList<string> role = await _userManager.GetRolesAsync(i);
                    if (role[0].Equals("Admin"))
                    {
                        emailAdmin.Add(i.Email);
                    }
                }
                var contentAdmin = string.Format("You have a new email from the customer, <a href='" + Url.Action("Detals", "ContactAdmin", new { id = contactAdmin.ContactAdminId }) + "'>click here to reply</a>.");
                var messageAdmin = new EmailMessage(emailAdmin, "Locked out account information", contentAdmin, null);

                await _emailSender.SendEmailAsync(messageUser);
                await _emailSender.SendEmailAsync(messageAdmin);

                ViewBag.Message = $"Hello {user.FirstName} {user.LastName}! Thank you for emailing us. We will review and respond as soon as possible.";
                return RedirectToAction(nameof(ContactAdmin));
            }

            return View(contactAdminRequest);
        }

        private async Task UpdateRole(User user, string role)
        {
            var roleUser = await _userManager.GetRolesAsync(user); // get role user
            var removeRole = await _userManager.RemoveFromRoleAsync(user, roleUser[0]); // remove user old role

            if (removeRole.Succeeded)
            {

                await _userManager.AddToRoleAsync(user, role); // add role for user
            }
        }

        [HttpGet]
        [Route("Payment")]
        public async Task<IActionResult> Payment()
        {
            List<Gateway> gateways = await _applicationDbContext.Gateways.ToListAsync();

            GatewayVM gatewayVM = new GatewayVM
            {
                Gateway = new Gateway(),
                Gateways = gateways
            };

            return View(gatewayVM);
        }

        [HttpGet]
        [Route("OrderHistory")]
        public async Task<IActionResult> OrderHistory()
        {
            User user = await _userManager.GetUserAsync(User);
            List<OrderDetail> orderDetails = await _applicationDbContext.InvoiceDetails
                                                        .Include(id => id.Invoice)
                                                        .Include(id => id.Product)
                                                            .ThenInclude(p => p.Photos)
                                                        .Include(id => id.Product)
                                                            .ThenInclude(p => p.Category)
                                                        .Where(id => id.Invoice.BuyerId == user.Id)
                                                        .ToListAsync();


            ViewBag.OrderDetails = orderDetails;

            return View();

        }
    }
}
