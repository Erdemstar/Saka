using Microsoft.AspNetCore.Mvc;

namespace information_leakage_comment.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Attack()
    {
        ViewBag.showResult = false;
        return View();
    }

    [HttpPost]
    public IActionResult Attack(IFormCollection form)
    {
        ViewBag.showResult = true;
        
        var email = form["email"].ToString();
        var password = form["password"].ToString();

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ViewBag.status = "Please fill email and password";
        }
        else if (email == "admin@saka.com" && password == "123456")
        {
            ViewBag.status = "Welcome Admin user";
        }
        else
        {
            ViewBag.status = "Email or Password is wrong. Please try again";
        }
        
        return View();
    }
}