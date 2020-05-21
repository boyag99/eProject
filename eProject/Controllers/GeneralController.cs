using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eProject.Data;
using eProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eProject.Controllers
{
    [Route("General")]
    public class GeneralController : Controller
    {


        //For Delivery Information
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
           
            return View();
        }


        // For FAQ
        [Route("IndexTerm")]
        public IActionResult IndexTerm()
        {
          
            return View();
        }

        //For Terms and Conditions
        [Route("IndexCondition")]
        public IActionResult IndexCondition()
        {

            return View();
        }


    }
}