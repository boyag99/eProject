using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

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

        [Route("")]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> Checkout(ShippingAddress shippingAddress)
        //{
        //    List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
        //    var user = User.FindFirst(ClaimTypes.Name);
        //    if (user == null)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var customer = _applicationDbContext.Users.SingleOrDefault(a => a.UserName.Equals(user.Value));

        //            //Create new invoice
        //            var invoice = new Invoice()
        //            {
        //                Name = "Invoice Online",
        //                Created = DateTime.Now,
        //                Status = 1,
        //                BuyerId = customer.Id
        //            };
        //            _applicationDbContext.Invoices.Add(invoice);
        //            _applicationDbContext.SaveChanges();
        //            //Create invoice details

        //            foreach (var item in cart)
        //            {
        //                var invoiceDetails = new OrderDetail
        //                {
        //                    InvoiceId = invoice.Id,
        //                    ProductId = item.ItemId,
        //                    Price = item.Price,
        //                    Quantity = 1
        //                };

        //                var seller = _applicationDbContext.Products.Include(p => p.User).FirstOrDefault(p => p.ProductId == item.ItemId);

        //                var invoiceToUpdate = _applicationDbContext.Invoices.FirstOrDefault(p => p.Id == invoice.Id);

        //                invoiceToUpdate.SellerId = seller.User.Id;

        //                _applicationDbContext.Invoices.Update(invoiceToUpdate);
        //                _applicationDbContext.InvoiceDetails.Add(invoiceDetails);
        //                _applicationDbContext.SaveChanges();
        //            }

        //            var content = string.Format("Thank you for your purchase, your order code is {0}", invoice.Id);
        //            var message = new EmailMessage(new string[] { customer.Email }, "Locked out account information", content, null);
        //            await _emailSender.SendEmailAsync(message);
        //            return RedirectToAction("GetInvoice", "Checkout");
        //        }
        //    }
        //    else
        //    {
        //        var customer = _applicationDbContext.Users.SingleOrDefault(a => a.UserName.Equals(user.Value));

        //        //Create new invoice
        //        var invoice = new Invoice()
        //        {
        //            Name = "Invoice Online",
        //            Created = DateTime.Now,
        //            Status = 1,
        //            BuyerId = customer.Id
        //        };
        //        _applicationDbContext.Invoices.Add(invoice);
        //        _applicationDbContext.SaveChanges();
        //        //Create invoice details

        //        foreach (var item in cart)
        //        {
        //            var invoiceDetails = new OrderDetail
        //            {
        //                InvoiceId = invoice.Id,
        //                ProductId = item.ItemId,
        //                Price = item.Price,
        //                Quantity = 1
        //            };

        //            var seller = _applicationDbContext.Products.Include(p => p.User).FirstOrDefault(p => p.ProductId == item.ItemId);

        //            var invoiceToUpdate = _applicationDbContext.Invoices.FirstOrDefault(p => p.Id == invoice.Id);

        //            invoiceToUpdate.SellerId = seller.User.Id;

        //            _applicationDbContext.Invoices.Update(invoiceToUpdate);
        //            _applicationDbContext.InvoiceDetails.Add(invoiceDetails);
        //            _applicationDbContext.SaveChanges();
        //        }

        //        var content = string.Format("Thank you for your purchase, your order code is {0}", invoice.Id);
        //        var message = new EmailMessage(new string[] { customer.Email }, "Locked out account information", content, null);
        //        await _emailSender.SendEmailAsync(message);
        //        return RedirectToAction("GetInvoice", "Checkout");
        //    }

        //}

        [HttpPost]
        [Route("Checkout")]
        public async Task<IActionResult> Checkout(CheckoutVM checkoutVM)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            

            User user = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    if (checkoutVM.ShippingAddress.Different is true)
                    {
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "invoices", new Item { ShippingAddress = checkoutVM.ShippingAddress });
                        //Create new invoice
                        var invoice = new Invoice()
                        {
                            Name = "Invoice Online",
                            Created = DateTime.Now,
                            Status = 1,
                            ShippingAddress = new ShippingAddress
                            {
                                FirstName = checkoutVM.ShippingAddress.FirstName,
                                LastName = checkoutVM.ShippingAddress.LastName,
                                StreetAddress = checkoutVM.ShippingAddress.StreetAddress,
                                City = checkoutVM.ShippingAddress.City,
                                Country = checkoutVM.ShippingAddress.Country,
                                PhoneNumber = checkoutVM.ShippingAddress.PhoneNumber,
                                PostalCode = checkoutVM.ShippingAddress.PostalCode,
                                Email = checkoutVM.ShippingAddress.Email
                            }
                        };
                        _applicationDbContext.Invoices.Add(invoice);
                        _applicationDbContext.SaveChanges();
                        //Create invoice details

                        foreach (var item in cart)
                        {
                            
                            var invoiceDetails = new OrderDetail
                            {
                                InvoiceId = invoice.Id,
                                ProductId = item.ItemId,
                                Price = item.Price,
                                Quantity = 1,
                                Note = checkoutVM.ShippingAddress.Note
                            };

                            var seller = _applicationDbContext.Products.Include(p => p.User).FirstOrDefault(p => p.ProductId == item.ItemId);

                            var invoiceToUpdate = _applicationDbContext.Invoices.FirstOrDefault(p => p.Id == invoice.Id);

                            invoiceToUpdate.SellerId = seller.User.Id;

                            _applicationDbContext.Invoices.Update(invoiceToUpdate);
                            _applicationDbContext.InvoiceDetails.Add(invoiceDetails);
                            _applicationDbContext.SaveChanges();
                        }

                        var content = string.Format("Thank you for your purchase, your order code is {0}", invoice.Id);
                        var message = new EmailMessage(new string[] { user.Email }, "Locked out account information", content, null);
                        await _emailSender.SendEmailAsync(message);
                        return RedirectToAction("GetInvoice", "Checkout");
                    }
                    else
                    {
                        SessionHelper.SetObjectAsJson(HttpContext.Session, "invoices", new Item { BuyerId = user.Id});
                        //Create new invoice
                        var invoice = new Invoice()
                        {
                            Name = "Invoice Online",
                            Created = DateTime.Now,
                            Status = 1,
                            BuyerId = user.Id
                        };
                        _applicationDbContext.Invoices.Add(invoice);
                        _applicationDbContext.SaveChanges();
                        //Create invoice details

                        foreach (var item in cart)
                        {
                            var invoiceDetails = new OrderDetail
                            {
                                InvoiceId = invoice.Id,
                                ProductId = item.ItemId,
                                Price = item.Price,
                                Quantity = 1,
                                Note = checkoutVM.ShippingAddress.Note
                            };

                            var seller = _applicationDbContext.Products.Include(p => p.User).FirstOrDefault(p => p.ProductId == item.ItemId);

                            var invoiceToUpdate = _applicationDbContext.Invoices.FirstOrDefault(p => p.Id == invoice.Id);

                            invoiceToUpdate.SellerId = seller.User.Id;

                            _applicationDbContext.Invoices.Update(invoiceToUpdate);
                            _applicationDbContext.InvoiceDetails.Add(invoiceDetails);
                            _applicationDbContext.SaveChanges();
                        }

                        var content = string.Format("Thank you for your purchase, your order code is {0}", invoice.Id);
                        var message = new EmailMessage(new string[] { user.Email }, "Locked out account information", content, null);
                        await _emailSender.SendEmailAsync(message);
                        return RedirectToAction("GetInvoice", "Checkout");
                    }

                }
                else
                {
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "invoices", new Item { ShippingAddress = checkoutVM.ShippingAddress });
                    //Create new invoice
                    var invoice = new Invoice()
                    {
                        Name = "Invoice Online",
                        Created = DateTime.Now,
                        Status = 1,
                        ShippingAddress = new ShippingAddress
                        {
                            FirstName = checkoutVM.ShippingAddress.FirstName,
                            LastName = checkoutVM.ShippingAddress.LastName,
                            StreetAddress = checkoutVM.ShippingAddress.StreetAddress,
                            City = checkoutVM.ShippingAddress.City,
                            Country = checkoutVM.ShippingAddress.Country,
                            PhoneNumber = checkoutVM.ShippingAddress.PhoneNumber,
                            PostalCode = checkoutVM.ShippingAddress.PostalCode,
                            Email = checkoutVM.ShippingAddress.Email
                        }
                    };
                    _applicationDbContext.Invoices.Add(invoice);
                    _applicationDbContext.SaveChanges();
                    //Create invoice details

                    foreach (var item in cart)
                    {
                        var invoiceDetails = new OrderDetail
                        {
                            InvoiceId = invoice.Id,
                            ProductId = item.ItemId,
                            Price = item.Price,
                            Quantity = 1,
                            Note = checkoutVM.ShippingAddress.Note
                        };

                        var seller = _applicationDbContext.Products.Include(p => p.User).FirstOrDefault(p => p.ProductId == item.ItemId);

                        var invoiceToUpdate = _applicationDbContext.Invoices.FirstOrDefault(p => p.Id == invoice.Id);

                        invoiceToUpdate.SellerId = seller.User.Id;

                        _applicationDbContext.Invoices.Update(invoiceToUpdate);
                        _applicationDbContext.InvoiceDetails.Add(invoiceDetails);
                        _applicationDbContext.SaveChanges();
                    }

                    var content = string.Format("Thank you for your purchase, your order code is {0}", invoice.Id);
                    var message = new EmailMessage(new string[] { checkoutVM.ShippingAddress.Email }, "Locked out account information", content, null);
                    await _emailSender.SendEmailAsync(message);
                    return RedirectToAction("GetInvoice", "Checkout");
                }
            }


            return View(checkoutVM);
        }

        [Route("GetInvoice")]
        public async Task<IActionResult> GetInvoice()
        {
            User currentUser = await _userManager.GetUserAsync(User);
            User user = null;
            if (currentUser != null) {
                user = await _applicationDbContext.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Id.Equals(currentUser.Id));
            }

            

            List<Product> products = new List<Product>();
            List<Item> carts = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            Item invoices = SessionHelper.GetObjectFromJson<Item>(HttpContext.Session, "invoices");

            if(invoices.ShippingAddress is null)
            {
                invoices.ShippingAddress = new ShippingAddress();
            }

            foreach (Item cart in carts)
            {
                Product product = _applicationDbContext.Products.Include(p => p.Category).Include(p => p.User).FirstOrDefault(p => p.ProductId == cart.ItemId);
                products.Add(product);
            }

            ViewBag.Buyer = user;
            ViewBag.Products = products;
            ViewBag.WareHouseAddress = await _applicationDbContext.WareHouseAddresses.FirstOrDefaultAsync();
            
            if(user != null)
            {

                if (invoices.ShippingAddress.Different)
                {
                    ViewBag.Invoice = invoices.ShippingAddress;
                }
                else
                {
                    ViewBag.Invoice = user;
                }
            } 
            else
            {
                ViewBag.Invoice = invoices.ShippingAddress;
                var test = ViewBag.Invoice;
            }

            return View();

        }
    }
}