using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Data;
using eProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/Review")]
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ReviewController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            List<Review> reviews = await _applicationDbContext.Reviews.Include(r => r.User).Include(r => r.Product).ToListAsync();
            return View(reviews);
        }

        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            Review review = await _applicationDbContext.Reviews.FirstOrDefaultAsync(r => r.ReviewId == id);

            if(review is null)
            {
                return RedirectToAction(nameof(Index));
            }

            review.Status = Review.ReviewStatus.NotApproved;

            await _applicationDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
