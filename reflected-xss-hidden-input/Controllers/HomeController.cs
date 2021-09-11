using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using reflected_xss_hidden_input.Models;
using reflected_xss_input.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace reflected_xss_hidden_input.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Attack()
        {
            return View(new Username() { Name="" });
        }

        [HttpPost]
        public IActionResult Attack(Username username)
        {
            return View(username);
        }
    }
}
