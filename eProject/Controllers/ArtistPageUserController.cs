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
    [Route("ArtistPageUser")]
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

            ArtistPageUserVM artistPageUserVM = new ArtistPageUserVM
            {
                Users = roleUser.ToPagedList(pageNumber, 12)
            };

            return View(artistPageUserVM);
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

        [Route("Search")]
        public IActionResult Search(int? page, ArtistPageUserVM artistPageUserVM)
        {
            var pageNumber = page ?? 1;
            List<User> roleUser = new List<User>();
            List<User> users = _applicationDbContext.Users.Include(u => u.Address).Where(u => u.Gender == artistPageUserVM.SearchArtistRequest.Gender).ToList();

            if(artistPageUserVM.SearchArtistRequest.Country != null)
            {
                users = users.Where(u => u.Address.Country.Equals(artistPageUserVM.SearchArtistRequest.Country)).ToList();
            }

            foreach (User user in users)
            {
                if (_userManager.IsInRoleAsync(user, "Artist").Result)
                {
                    roleUser.Add(user);
                }
            }
            ArtistPageUserVM artistPageUser = new ArtistPageUserVM
            {
                Users = roleUser.ToPagedList(pageNumber, 12)
            };
            return View("Index", artistPageUser);
        }
    }
}