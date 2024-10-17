using insecure_direct_object_reference_view_profile.Core.Interface.Repository.Account;
using Microsoft.AspNetCore.Mvc;

namespace insecure_direct_object_reference_view_profile.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAccountRepository _accountRepository;

    public HomeController(ILogger<HomeController> logger, IAccountRepository accountRepository)
    {
        _logger = logger;
        _accountRepository = accountRepository;
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
        return Redirect($"/Home/Attack/{accountEntity.Id}");
    }
    
    [HttpGet]
    public async Task<IActionResult> Attack(string Id)
    {
        var userId = HttpContext.Session.GetString("UserSession");
        if (userId is null) return RedirectToAction("Login");

        var account = await _accountRepository.GetByIdAsync(Id);
        if (account is null) return RedirectToAction("Login");
        
        return View(account);
    }
    
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
    
    
    
}