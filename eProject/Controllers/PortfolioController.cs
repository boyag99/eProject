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
using X.PagedList;

namespace eProject.Controllers
{
    [Authorize]
    [Route("Portfolio")]
    public class PortfolioController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<User> _userManager;

        public PortfolioController(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            User user = await _userManager.GetUserAsync(User);

            List<Porfolio> porfolios = await _applicationDbContext.Porfolios
                                                .Include(p => p.User)
                                                .Include(p => p.Product)
                                                    .ThenInclude(p => p.Photos)
                                                .Include(p => p.Product)
                                                    .ThenInclude(p => p.Category)
                                                .Where(p => p.UserId.Equals(user.Id))
                                                .ToListAsync();
            ViewBag.page = porfolios.ToPagedList(pageNumber, 12);
            return View(porfolios);
        }

        [HttpGet]
        [Route("Add")]
        public async Task<IActionResult> Add(int id, string returnController, string returnAction)
        {
            Porfolio currentPorfolio = await _applicationDbContext.Porfolios.FirstOrDefaultAsync(p => p.ProductId == id);
            Product product = await _applicationDbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            User user = await _userManager.GetUserAsync(User);

            if (currentPorfolio != null)
            {
                return RedirectToAction(returnAction, returnController);
            }

            if (product is null)
            {
                return RedirectToAction(returnAction, returnController);
            }


            Porfolio porfolio = new Porfolio
            {
                ProductId = product.ProductId,
                UserId = user.Id
            };

            _applicationDbContext.Porfolios.Add(porfolio);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}