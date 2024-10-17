using Microsoft.AspNetCore.Mvc;

namespace client_side_restriction_hidden_form.Mvc.Controllers;

public class HomeController : Controller
{
    private static string _email = "";
    private static string _name = "";
    private static string _lastname = "";
    private static string _hobby = "";
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
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(IFormCollection form)
    {
        var email = form["email"].ToString();
        var password = form["password"].ToString();

        if (email != "user@saka.com" || password != "pass")
        {
            _email = "";
            _name = "";
            _lastname = "";
            _hobby = "";
            return View();
        }

        _email = "user@saka.com";
        _name = "Saka User";
        _lastname = "NPC";
        _hobby = "I like playing basketball";
        return Redirect(nameof(Attack));
    }

    [HttpGet]
    public IActionResult Attack()
    {
        if (string.IsNullOrEmpty(_email))
            return Redirect(nameof(Login));

        TempData["email"] = _email;
        TempData["name"] = _name;
        TempData["lastname"] = _lastname;
        TempData["hobby"] = _hobby;
        return View();
    }

    [HttpPost]
    public IActionResult Attack(IFormCollection form)
    {
        var hobby = form["hobby"].ToString();
        _hobby = hobby;
        return View();
    }
}