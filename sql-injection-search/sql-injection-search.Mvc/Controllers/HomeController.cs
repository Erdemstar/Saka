using Microsoft.AspNetCore.Mvc;
using sql_injection_login_bypass.Core.Interface.Repository.Account;

namespace sql_injection_search.Mvc.Controllers;

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
    public IActionResult Attack()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Attack(IFormCollection form)
    {
        var username = form["username"].ToString();
        var accounts = await _accountRepository.GetByUsernameAsync(username);
        return View(accounts);
    }
}