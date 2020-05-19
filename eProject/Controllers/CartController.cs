using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using eProject.App.Data;

using Microsoft.AspNetCore.Razor.Language;
using eProject.Models;
using eProject.App.Helpers;
using System.Security.Claims;

namespace eProject.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CartController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }
     
        [Route("Index")]
        public IActionResult Index()
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                ViewBag.countItems = 0;
                ViewBag.Total = 0;
            }
            else
            {
                ViewBag.countItems = cart.Count;
                ViewBag.Toatal = cart.Sum(it => it.Price * it.Quantity);
            }
                
            return View();
        }
       [HttpGet]
        [Route("Buy/{id}")]
        public IActionResult Buy(int id)
        {
            var product = _applicationDbContext.Products.Find(id);
            var photo = product.Photos.SingleOrDefault(p => p.Status && p.Featured);
            var photoName = photo == null ? "no-image.jpg" : photo.Name;
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                var cart = new List<Item>();
                cart.Add(new Item
                {
                    ItemId = product.ProductId,
                    Name = product.Name,
                    Price = product.Price,
                    Photo = photoName,
                    Quantity = 1
                }); ;
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = checkexist(id, cart);
                if (index == -1)
                {
                    cart.Add(new Item
                    {
                       ItemId= product.ProductId,
                       Name = product.Name,
                       Price = product.Price,
                       Photo = photoName,
                        Quantity = 1
                });
                   
                }
                else
                {
                    cart[index].Quantity++;
                }
            }
            return RedirectToAction("Index", "Cart");
        }
        [HttpPost]
        [Route("Buy/{id}")]
        public IActionResult Buy(int id, int quantity)
        {
            var product = _applicationDbContext.Products.Find(id);
            var photo = product.Photos.SingleOrDefault(p => p.Status && p.Featured);
            var photoName = photo == null ? "no-image.jpg" : photo.Name;
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                var cart = new List<Item>();
                cart.Add(new Item
                {
                    ItemId = product.ProductId,
                    Name = product.Name,
                    Price = product.Price,
                    Photo = photoName,
                    Quantity = quantity
                }); ;
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = checkexist(id, cart);
                if (index == -1)
                {
                    cart.Add(new Item
                    {
                        ItemId = product.ProductId,
                        Name = product.Name,
                        Price = product.Price,
                        Photo = photoName,
                        Quantity = quantity
                    });

                }
                else
                {
                    cart[index].Quantity += quantity;
                }
            }
            return RedirectToAction("Index", "Cart");
        }
        [HttpPost]
        [Route("Update")]
        public IActionResult Update(int[] quantity)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                cart[i].Quantity = quantity[i];
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index", "Cart");
        }
        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = checkexist(id, cart);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart",cart);
            return RedirectToAction("Index", "cart");
        }
        [Route("checkout")]
        public IActionResult Checkout()
        {
            var user = User.FindFirst(ClaimTypes.Name);
            if (user == null)
            {
                return RedirectToAction("Home", "Login");
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
                    UserId = customer.Id
                };
                _applicationDbContext.Invoices.Add(invoice);
                _applicationDbContext.SaveChanges();
                //Create invoice details
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                foreach (var item in cart)
                {
                    var invoiceDetails = new InvoiceDetail
                    {
                        InvoiceId = invoice.Id,
                        ProductId = item.ItemId,
                        Price = item.Price,
                        Quantity = item.Quantity
                    };
                    _applicationDbContext.InvoiceDetails.Add(invoiceDetails);
                    _applicationDbContext.SaveChanges();
                }
                //Remove items in cart
                HttpContext.Session.Remove("cart");
                return RedirectToAction("Thanks", "Cart");
            }
            
        }
        [Route("thanks")]
        public IActionResult Thanks()
        {
            return View("Thanks");
        }
        private int checkexist(int id, List<Item> cart)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ItemId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
       
    }
}