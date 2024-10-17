using cross_site_request_forgery_password_change.Core.Interface.Repository.Account;
using Microsoft.AspNetCore.Mvc;

namespace cross_site_request_forgery_password_change.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IAccountRepository accountRepository,
        ILogger<HomeController> logger)
    {
        _accountRepository = accountRepository;
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
    public async Task<IActionResult> Login(IFormCollection form)
    {
        var email = form["email"].ToString();
        var password = form["password"].ToString();

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ViewBag.LoginError = "Username or Password is null. Please fill it and send again";
            return View();
        }

        var accountEntity = await _accountRepository.GetByEmailPasswordAsync(email, password);
        if (accountEntity is null)
        {
            ViewBag.LoginError = "Username or Password is wrong. Please control it";
            return View();
        }

        HttpContext.Session.SetString("UserSession", accountEntity.Id);
        return RedirectToAction("Attack");
    }

    [HttpGet]
    public async Task<IActionResult> Attack()
    {
        var userId = HttpContext.Session.GetString("UserSession");
        if (userId is null)
        {
            return RedirectToAction("Login");
        }
        
        var accountEntity = await _accountRepository.GetByIdAsync(userId);
        if (accountEntity is null)
        {
            ViewBag.AttackError = "There is a problem while getting current user account information";
            return View(null);
        }

        return View(accountEntity);
    }

    [HttpPost]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Attack(IFormCollection form)
    {
        var userId = HttpContext.Session.GetString("UserSession");
        if (userId is null)
            return RedirectToAction("Login");

        var password = form["password"].ToString();
        var password2 = form["password2"].ToString();

        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(password2))
        {
            ViewBag.AttackError = "Password is null. Please fill it and send again";
            return View(null);
        }

        if (password != password2)
        {
            ViewBag.AttackError = "Password is null. Please fill it and send again";
            return View(null);
        }

        var accountEntity = await _accountRepository.GetByIdAsync(userId);
        if (accountEntity is null)
        {
            ViewBag.AttackError = "There is a problem while getting current user account information";
            return View(null);
        }

        accountEntity.Password = password;
        await _accountRepository.UpdateAsync(accountEntity);

        return View(accountEntity);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}