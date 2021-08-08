using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using reflected_xss_referer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace reflected_xss_referer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Attack()
        {
            string referer = Request.Headers["Referer"].ToString();
            ViewBag.referer = referer;
            return View();
        }

        [HttpPost]
        public IActionResult Attack(string temporay)
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
