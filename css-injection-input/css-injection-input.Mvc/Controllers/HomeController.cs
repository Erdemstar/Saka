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
        ViewBag.color = "black";
        ViewBag.tag = "h1";

        return View();
    }

    [HttpPost]
    public IActionResult Attack(IFormCollection form)
    {
        var color = form["color"].ToString();
        var tag = form["tag"].ToString();
        if (string.IsNullOrEmpty(color))
            color = "black";

        if (string.IsNullOrEmpty(tag))
            tag = "h1";

        ViewBag.color = color;
        ViewBag.tag = tag;

        return View();
    }
}