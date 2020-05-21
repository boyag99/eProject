using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Data;
using eProject.Models;
using eProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace eProject.Controllers
{
    [Route("product")]
    public class ProductUserController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<User> _userManager;

        public ProductUserController(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;

        }
        [Route("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
  
            Product product = _applicationDbContext.Products.Include(p=>p.Photos).Include(p => p.Category)
                .Include(p => p.User)
                .ThenInclude(u => u.Address)
                .FirstOrDefault(p=>p.ProductId ==id);

            AuctionHistory auctionHistory = _applicationDbContext.AuctionHistories.Include(ah => ah.User).OrderByDescending(ah => ah.Bid).FirstOrDefault(ah => ah.ProductId == id);



            if (product is null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (auctionHistory is null)
            {
                if(product.ToDate < DateTime.Now && product.Auction == true)
                {
                    product.ToDate = product.ToDate.AddDays(7);
                }
            }

            product.Hot += 1;
            _applicationDbContext.Products.Update(product);
            _applicationDbContext.SaveChanges();

            List<Product> products = _applicationDbContext.Products.OrderByDescending(p => p.Hot).Take(1).ToList();
            ViewBag.Hot = false;
            foreach (var item in products)
            {
                if(item.ProductId == product.ProductId)
                {
                    if(product.Hot == 0)
                    {
                        ViewBag.Hot = false;
                    } else
                    {
                        ViewBag.Hot = true;
                    }
                }
            }

            var featuredPhoto = product.Photos.SingleOrDefault(p => p.Status && p.Featured);
            ViewBag.Product = product;
            ViewBag.FeaturedPhoto = featuredPhoto == null ? "no-image.jpg" : featuredPhoto.Name;
            ViewBag.ProductImages = product.Photos.Where(p => p.Status).ToList();
            ViewBag.RelatedProduct = _applicationDbContext.Products.Include(p => p.Photos).Where(p => p.CategoryId == product.CategoryId && p.ProductId != id && p.Status).ToList();
            ViewBag.Reviews = await _applicationDbContext.Reviews.Include(r => r.User).Where(r => r.ProductId == id).ToListAsync();
            ViewBag.MaxBid = _applicationDbContext.AuctionHistories.OrderBy(ah => ah.Bid).FirstOrDefault(ah => ah.ProductId == id);
            ViewBag.AuctionHistories = await _applicationDbContext.AuctionHistories.Include(ah => ah.User).Where(ah => ah.ProductId == id).ToListAsync();
            ViewBag.AuctionHistory = auctionHistory;

            return View("Details");
        }

        [Route("index")]
        public IActionResult Index(int? page, string currentSearch, string search = null)
        {
            var pageNumber = page ?? 1;

            if(search != null)
            {
                pageNumber = 1;
            }else
            {
                search = currentSearch;
            }

            ViewData["CurrentSearch"] = search;

            var product = _applicationDbContext.Products.Include(p => p.Photos).Where(p => p.Status);

            if (!string.IsNullOrEmpty(search))
            {
                product = product.Where(p => p.Name.ToLower().Contains(search.Trim().ToLower()));
            }

            ViewBag.Products = product.ToPagedList(pageNumber, 12);

            List<Product> hotProducts = _applicationDbContext.Products.Include(p => p.Photos).OrderByDescending(p => p.Hot).Take(4).ToList();
            ViewBag.Hot = hotProducts;

            List<Product> newProducts = _applicationDbContext.Products.Include(p => p.Photos).OrderByDescending(p => p.FromDate).Take(4).ToList();
            ViewBag.NewProducts = newProducts;

            List<Product> saleProducts = _applicationDbContext.Products.Include(p => p.Photos).Where(p=>p.SalePrice>0).Take(4).ToList();
            ViewBag.SaleProducts = saleProducts;


            return View("Index");
        }
        [Route("category/{id}")]
        public IActionResult Category(int id, int? page, string currentSearch, string search = null)
        {
            var pageNumber = page ?? 1;
            if (search != null)
            {
                pageNumber = 1;
            }
            else
            {
                search = currentSearch;
            }
            ViewData["CurrentSearch"] = search;
            var product = _applicationDbContext.Products
                .Include(p => p.Photos)
                .Where(p => p.CategoryId == id);
            if (!string.IsNullOrEmpty(search))
            {
                product = product.Where(p => p.Name.ToLower().Contains(search.Trim().ToLower()));
            }

            ViewBag.Products = product.ToPagedList(pageNumber, 12);

            ViewBag.Category = _applicationDbContext.Categories.Include(c => c.Products).SingleOrDefault(p => p.CategoryId == id);

            ViewBag.CountProducts = _applicationDbContext.Products
                .Where(p => p.CategoryId == id)
                .Count(p => p.Status);

            List<Product> hotProducts = _applicationDbContext.Products.Include(p => p.Photos).OrderByDescending(p => p.Hot).Take(4).ToList();
            ViewBag.Hot = hotProducts;

            List<Product> newProducts = _applicationDbContext.Products.Include(p => p.Photos).OrderByDescending(p => p.FromDate).Take(4).ToList();
            ViewBag.NewProducts = newProducts;

            List<Product> saleProducts = _applicationDbContext.Products.Include(p => p.Photos).Where(p => p.SalePrice > 0).Take(4).ToList();
            ViewBag.SaleProducts = saleProducts;

            return View("Category");
        }

        [HttpPost]
        [Route("Review/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Review(int id , ProductUserVM productUserVM)
        {
            User user = await _userManager.GetUserAsync(User);

            if(user is null)
            {
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                Review review = new Review
                {
                    Message = productUserVM.ReviewRequest.Message,
                    UserId = user.Id
                };

                _applicationDbContext.Reviews.Add(review);
                await _applicationDbContext.SaveChangesAsync();

                return RedirectToAction("Details", "ProductUser", new { id = id });
            }

            return RedirectToAction("Details", "ProductUser", new { id = id });
        }
    }
}