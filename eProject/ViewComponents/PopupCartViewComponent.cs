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
    [ViewComponent(Name = "PopupCart")]
    public class PopupCartViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public PopupCartViewComponent(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                ViewBag.CountItems = 0;
                ViewBag.SubTotal = 0;
            }
            else
            {
                List<Product> products = new List<Product>();
                List<Item> carts = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                ViewBag.CountItems = carts.Count;
                ViewBag.SubTotal = carts.Sum(it => it.Price);

            }

            return View("Index");
        }

    }
}
