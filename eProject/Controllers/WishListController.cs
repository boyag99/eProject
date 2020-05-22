using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Data;
using eProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eProject.Controllers
{

    [Authorize]
    [Route("WishList")]
    public class WishListController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<User> _userManager;

        public WishListController(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]

        public async Task<IActionResult> Index()
        {
            User user = await _userManager.GetUserAsync(User);

            List<WishList> wishList = await _applicationDbContext.WishLists
                                                .Include(p => p.User)
                                                .Include(p => p.Product)
                                                    .ThenInclude(p => p.Photos)
                                                .Include(p => p.Product)
                                                    .ThenInclude(p => p.Category)
                                                .Where(p => p.UserId.Equals(user.Id))
                                                .ToListAsync();

            return View(wishList);
            
        }

        [HttpGet]
        [Route("Add")]
        public async Task<IActionResult> Add(int id, string returnController, string returnAction)
        {
            WishList currentWishList = await _applicationDbContext.WishLists.FirstOrDefaultAsync(w => w.ProductId == id);
            Product product = await _applicationDbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            User user = await _userManager.GetUserAsync(User);

            if (currentWishList != null)
            {
                return RedirectToAction(returnAction, returnController);
            }

            if (product is null)
            {
                return RedirectToAction(returnAction, returnController);
            }


            WishList wishList = new WishList
            {
                ProductId = product.ProductId,
                UserId = user.Id
            };

            _applicationDbContext.WishLists.Add(wishList);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Route("remove/{id}")]
        public IActionResult Remove(int id)
        {
            WishList wishList = _applicationDbContext.WishLists.SingleOrDefault(w => w.WishListId == id);

            _applicationDbContext.WishLists.Remove(wishList);
            _applicationDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}