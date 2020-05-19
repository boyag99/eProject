using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.App.Helpers;
using eProject.Data;
using eProject.Models;
using eProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eProject.Controllers
{
    [Authorize]
    [Route("Artist")]
    public class ArtistController : Controller
    {
        private const string USER_PATH = "backend/images/users";
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<User> _userManager;

        public ArtistController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        
    }
}
