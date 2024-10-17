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
    public IActionResult Attack([FromQuery] string url)
    {
        if (string.IsNullOrEmpty(url))
            return View();
        return Redirect(url);
    }
    
    
}