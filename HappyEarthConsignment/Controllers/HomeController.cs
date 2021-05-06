/*HomeController with action methods for displaying site home page and about us page
Authors: Amanda MacGregor & Tara Schoenherr
References: Demo projects from LSV
Prepared: Spring 2021
Purpose: CIS 665 ASP Project
 */

using HappyEarthConsignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HappyEarthConsignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //displays home page
        public IActionResult Index()
        {
            return View();
        }

        //displays about us page
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
