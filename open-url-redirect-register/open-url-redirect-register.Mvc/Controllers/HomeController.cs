using Microsoft.AspNetCore.Mvc;

namespace open_url_redirect_social.Mvc.Controllers;

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
    public IActionResult Attack(IFormCollection form)
    {
        var username = form["username"].ToString();
        var password = form["password"].ToString();
        var name = form["name"].ToString();
        var surname = form["surname"].ToString();
        var address = form["address"].ToString();
        var url = form["url"].ToString();

        TempData["status"] = "Register işlemi tamamlandı";
        
        return Redirect(url);
    }
    
    
}