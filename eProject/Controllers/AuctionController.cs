using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Route("Auction")]
    public class AuctionController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;

        public AuctionController(ApplicationDbContext applicationDbContext, UserManager<User> userManager, IEmailSender emailSender)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [HttpPost]
        [Route("Bid/{productId}")]
        public async Task<IActionResult> Bid(int productId, ProductUserVM productUserVM)
        {
            Product product = await _applicationDbContext.Products.Include(p => p.User).FirstOrDefaultAsync(p => p.ProductId == productId);
            User user = await _userManager.GetUserAsync(User);

            List<AuctionHistory> auctionHistories = await _applicationDbContext.AuctionHistories
                .Include(ah => ah.Product)
                .Include(ah => ah.User)
                .Where(ah => ah.ProductId == productId)
                .ToListAsync();

            if(productUserVM.Product.SalePrice <= product.SalePrice)
            {
                ViewBag.Message = "Bid cannot be less than current price.";
                return RedirectToAction("Details", "ProductUser", new { id = productId });
            }

            if (user.Id.Equals(product.User.Id))
            {
                ViewBag.Message = "You cannot bid on your own products.";
                return RedirectToAction("Details", "ProductUser", new { id = productId });
            }

            if(product.ToDate > DateTime.Now)
            {
                product.SalePrice = productUserVM.Product.SalePrice;

                AuctionHistory auctionHistory = new AuctionHistory
                {
                    Bid = productUserVM.Product.SalePrice,
                    ProductId = product.ProductId,
                    UserId = user.Id
                };

                _applicationDbContext.AuctionHistories.Add(auctionHistory);
                _applicationDbContext.Products.Update(product);
                await _applicationDbContext.SaveChangesAsync();

                List<string> emailUser = new List<string>();

                foreach (var item in auctionHistories)
                {
                    emailUser.Add(item.User.Email);
                }

                var content = string.Format("Product ID is <a href='/product/details/{0}'>#{0}</a> you have a bid higher. Currently bid {1}.", productId, productUserVM.Product.SalePrice);
                if (emailUser.Count > 0)
                {
                    var message = new EmailMessage(emailUser.Distinct(), "Bid Information", content, null);
                    await _emailSender.SendEmailAsync(message);
                }

                return RedirectToAction("Details", "ProductUser", new { id = productId });
            }

            return RedirectToAction("Details", "ProductUser", new { id = productId });
        }
    }
}
