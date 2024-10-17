using Microsoft.AspNetCore.Mvc;

namespace authentication_bypass_cookie.Mvc.Controllers;

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
        ViewBag.role = HttpContext.Request.Cookies["Role"];
        return View();
    }
    
    [HttpPost]
    public IActionResult Attack(IFormCollection form)
    {
        
        var username = form["username"].ToString();
        var password = form["password"].ToString();

        if (username == "user" && password == "pass")
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(1), // Çerezin süresi
                HttpOnly = true, // Çerez yalnızca HTTP üzerinden erişilebilir
                Secure = true // Çerez yalnızca HTTPS üzerinden gönderilir
            };
            
            HttpContext.Response.Cookies.Append("Role", "user", cookieOptions);
            ViewBag.role = "user";
        }
        return View();
    }
    
}