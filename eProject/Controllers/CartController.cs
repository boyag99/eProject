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
using eProject.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace eProject.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<User> _userManager;

        public CartController(ApplicationDbContext applicationDbContext, IEmailSender emailSender, UserManager<User>userManager)
        {
            _applicationDbContext = applicationDbContext;
            _emailSender = emailSender;
            _userManager = userManager;

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
                ViewBag.Total = cart.Sum(it => it.Price);
                
            }
                
            return View();
        }

        [HttpGet]
        [Route("Buy/{id}")]
        public IActionResult Buy(int id)
        {
            var product = _applicationDbContext.Products
                .Include(p => p.Photos)
                .FirstOrDefault(p => p.ProductId == id);

            var photo = product.Photos.SingleOrDefault(p => p.Status == true && p.Featured == true);
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
                });
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
                    });
                }

                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }



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