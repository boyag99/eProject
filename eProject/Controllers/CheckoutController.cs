using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eProject.App.Helpers;
using eProject.Data;
using eProject.Models;
using eProject.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eProject.Controllers
{
    [Route("checkout")]
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<User> _userManager;

        public CheckoutController(ApplicationDbContext applicationDbContext, IEmailSender emailSender, UserManager<User> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _emailSender = emailSender;
            _userManager = userManager;

        }
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            var user = User.FindFirst(ClaimTypes.Name);
            if (user == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var customer = _applicationDbContext.Users.SingleOrDefault(a => a.UserName.Equals(user.Value));

                //Create new invoice
                var invoice = new Invoice()
                {
                    Name = "Invoice Online",
                    Created = DateTime.Now,
                    Status = 1,
                    BuyerId = customer.Id
                };
                _applicationDbContext.Invoices.Add(invoice);
                _applicationDbContext.SaveChanges();
                //Create invoice details
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                foreach (var item in cart)
                {
                    var invoiceDetails = new OrderDetail
                    {
                        InvoiceId = invoice.Id,
                        ProductId = item.ItemId,
                        Price = item.Price,
                        Quantity = 1
                    };

                    var seller = _applicationDbContext.Products.Include(p => p.User).FirstOrDefault(p => p.ProductId == item.ItemId);

                    var invoiceToUpdate = _applicationDbContext.Invoices.FirstOrDefault(p => p.Id == invoice.Id);

                    invoiceToUpdate.SellerId = seller.User.Id;

                    _applicationDbContext.Invoices.Update(invoiceToUpdate);
                    _applicationDbContext.InvoiceDetails.Add(invoiceDetails);
                    _applicationDbContext.SaveChanges();
                }

                var content = string.Format("Thank you for your purchase, your order code is {0}", invoice.Id);
                var message = new EmailMessage(new string[] { customer.Email }, "Locked out account information", content, null);
                await _emailSender.SendEmailAsync(message);
                //Remove items in cart
                //HttpContext.Session.Remove("cart");
                return RedirectToAction("GetInvoice", "Checkout");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Checkout(ShippingAddress shippingAddress)
        {
            var user = User.FindFirst(ClaimTypes.Name);
            if (user == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var customer = _applicationDbContext.Users.SingleOrDefault(a => a.UserName.Equals(user.Value));

                //Create new invoice
                var shipping = new ShippingAddress
                {
                    FName = shippingAddress.FName,
                    LName = shippingAddress.LName,
                    Address = shippingAddress.Address,
                    Country = shippingAddress.Country,
                    City = shippingAddress.City,
                    PostCode = shippingAddress.PostCode,
                    Email = shippingAddress.Email,
                    PhoneNumber = shippingAddress.PhoneNumber,
                    Note = shippingAddress.Note
                };


                return RedirectToAction("GetInvoice", "Checkout");
            }
        }

        [Route("GetInvoice")]
        public async Task<IActionResult> GetInvoice()
        {
            User user = await _userManager.GetUserAsync(User);

            List<Product> products = new List<Product>();
            List<Item> carts = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            foreach (Item cart in carts)
            {
                Product product = _applicationDbContext.Products.FirstOrDefault(p => p.ProductId == cart.ItemId);
                products.Add(product);
            }

            ViewBag.Buyer = user;
            ViewBag.Products = products;

            return View();

        }
    }
}