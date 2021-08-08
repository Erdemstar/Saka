using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using reflected_xss_tag_attribute_href.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace reflected_xss_tag_attribute_href.Controllers
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
            Models.Redirect redirect = new Models.Redirect() { url = "" };
            if (!String.IsNullOrEmpty(HttpContext.Request.Query["url"])) { 
                redirect.url = HttpContext.Request.Query["url"];
            }
            return View(redirect);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
