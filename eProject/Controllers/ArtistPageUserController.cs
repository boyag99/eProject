using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Data;
using eProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace eProject.Controllers
{
    [Route("ArtistPage")]
    public class ArtistPageUserController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<User> _userManager;

        public ArtistPageUserController(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }
        [Route("Index")]
        public IActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            List<User> roleUser = new List<User>();
            List<User> users = _applicationDbContext.Users.ToList();
            foreach(User user in users)
            {
                if(_userManager.IsInRoleAsync(user, "Artist").Result)
                {
                    roleUser.Add(user);
                }
            }
            ViewBag.Artists = roleUser.ToPagedList(pageNumber, 12);
            return View();
        }

        [Route("Detail/{id}")]
        public IActionResult Detail(string id)
        {
            User users = _applicationDbContext.Users
                .Include(u=>u.Address)
                .FirstOrDefault(u=>u.Id == id);
            ViewBag.Artist = users;
            ViewBag.Products = _applicationDbContext.Products
                .Include(p => p.Photos)
                .Include(p => p.User)
                .Where(p => p.User.Id.Equals(users.Id))
                .ToList();
            return View();
        }
    }
}