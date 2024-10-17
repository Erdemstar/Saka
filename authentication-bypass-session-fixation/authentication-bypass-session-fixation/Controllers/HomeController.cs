using Microsoft.AspNetCore.Mvc;

namespace authentication_bypass_session_fixation.Controllers;

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

        if (ModelState.IsValid)
        {
            // Kullanıcı doğrulaması
            var user = AuthenticateUser(username, password);
            if (user != null)
            {
                HttpContext.Session.SetString("User", username);
                return RedirectToAction("Profile", "Home");
            }
        }
        
        return View();
    }
    
    [HttpGet]
    public IActionResult Profile()
    {
        var username = HttpContext.Session.GetString("User");
        if (string.IsNullOrEmpty(username))
        {
            return RedirectToAction("Attack", "Home");
        }
        
        ViewBag.role = HttpContext.Request.Cookies["Role"];
        return View();
    }
    
    private bool AuthenticateUser(string username, string password)
    {
        // Basit bir doğrulama örneği
        if (username == "test" && password == "password")
        {
            return true;
        }
        return false;
    }
    
    // Çıkış işlemi
    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Attack", "Home");
    }
    
    
}