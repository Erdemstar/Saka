using cross_site_request_forgery_account_delete.Core.Interface.Repository.Account;
using Microsoft.AspNetCore.Mvc;

namespace cross_site_request_forgery_account_delete.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IAccountRepository accountRepository, ILogger<HomeController> logger)
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

        if (accountEntity.isDeleted)
        {
            ViewBag.LoginError = "Account is deleted already.";
            return View();
        }

        HttpContext.Session.SetString("UserSession", accountEntity.Id);
        return RedirectToAction("Attack");
    }

    [HttpGet]
    public async Task<IActionResult> Attack([FromQuery] string status)
    {
        var UserId = HttpContext.Session.GetString("UserSession");
        if (UserId is null) return RedirectToAction("Login");
        
        var accountEntity = await _accountRepository.GetByIdAsync(UserId);
        
        // status check if it's null it's like get request otherwise user saw content and send request
        if (string.IsNullOrEmpty(status))
        {
            if (accountEntity is null)
            {
                ViewBag.AttackError = "There is a problem while getting current user account information";
                return View(null);
            }

            return View(accountEntity);
        }

        // account status control
        if (status != "deleted")
        {
            ViewBag.AttackError = "There is a problem while getting current user account information";
            return View(null);
        }
        
        // update account
        accountEntity.isDeleted = true;
        await _accountRepository.UpdateAsync(accountEntity);

        return RedirectToAction("Logout");

    }
    
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}