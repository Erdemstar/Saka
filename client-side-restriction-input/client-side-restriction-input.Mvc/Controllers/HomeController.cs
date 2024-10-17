using Microsoft.AspNetCore.Mvc;

namespace client_side_restriction_input.Mvc.Controllers;

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
        return View();
    }

    [HttpPost]
    public IActionResult Attack(string email)
    {
        var random = new Random().Next(1, 10);
        ViewBag.email = email;
        ViewBag.random = random;
        return View();
    }
}