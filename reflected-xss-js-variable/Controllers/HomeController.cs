using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using reflected_xss_js_variable.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace reflected_xss_js_variable.Controllers
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

        public IActionResult Attack()
        {
            Models.Username username = new Models.Username() { Name = "" };

            if (!String.IsNullOrEmpty(HttpContext.Request.Query["Name"]))
            {
                username.Name = HttpContext.Request.Query["Name"];
            }
            return View(username);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
