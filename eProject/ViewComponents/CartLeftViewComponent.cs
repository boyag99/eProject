using eProject.App.Helpers;
using eProject.Data;
using eProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProject.ViewComponents
{
    [ViewComponent(Name ="CartLeft")]
    public class CartLeftViewComponent : ViewComponent
    {
        private ApplicationDbContext db;
        public CartLeftViewComponent(ApplicationDbContext _db)
        {
            this.db = _db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                ViewBag.countItems = 0;
                ViewBag.Total = 0;
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                ViewBag.countItems = cart.Count;
                ViewBag.Total = cart.Sum(it => it.Price);
            }
           
            return View("Index");
        }

    }
}
