using Microsoft.AspNetCore.Mvc;
using sql_injection_login_bypass.Core.Interface.Repository.Account;

namespace sql_injection_login_bypass.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IAccountRepository _accountRepository;

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
    public IActionResult Attack()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Attack(IFormCollection form)
    {
        var user = form["username"].ToString();
        var pass = form["password"].ToString();
        
        var isLogging = await _accountRepository.LoginAsync(user,pass);
        ViewBag.status = isLogging;
        return View();
    }
}