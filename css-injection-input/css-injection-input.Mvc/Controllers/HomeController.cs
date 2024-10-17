using Microsoft.AspNetCore.Mvc;

namespace css_injection_input.Mvc.Controllers;

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
        ViewBag.type = "primary";
        return View();
    }

    [HttpPost]
    public IActionResult Attack(IFormCollection form)
    {
        var type = form["type"].ToString();
        if (string.IsNullOrEmpty(type))
            type = "primary";

        ViewBag.type = type;
        
        return View();
    }
}