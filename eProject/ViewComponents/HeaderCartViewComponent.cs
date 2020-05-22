using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.App.Helpers;
using eProject.Data;
using eProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eProject.ViewComponents
{
    [ViewComponent(Name = "HeaderCart")]
    public class HeaderCartViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public HeaderCartViewComponent(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Product> products = new List<Product>();
                ViewBag.Products = products;
            }
            else
            {
                List<Product> products = new List<Product>();
                List<Item> carts = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                ViewBag.CountItems = carts.Count;
                ViewBag.Total = carts.Sum(it => it.Price);

                foreach (Item item in carts)
                {
                    Product product = await _applicationDbContext.Products.Include(p => p.Photos).FirstOrDefaultAsync(p => p.ProductId == item.ItemId);

                    products.Add(product);
                }

                ViewBag.Products = products;
               
            }

            return View("Index");
        }
    }
}
