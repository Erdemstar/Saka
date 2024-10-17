using cross_site_request_forgery_money_transfer.Core.Entity.View.AccountFinancial;
using cross_site_request_forgery_money_transfer.Core.Interface.Repository.Account;
using cross_site_request_forgery_money_transfer.Core.Interface.Repository.Financial;
using Microsoft.AspNetCore.Mvc;

namespace cross_site_request_forgery_money_transfer.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly IAccountRepository _accountRepository;
    private readonly IFinancialRepository _financialRepository;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IAccountRepository accountRepository, IFinancialRepository financialRepository,
        ILogger<HomeController> logger)
    {
        _accountRepository = accountRepository;
        _financialRepository = financialRepository;
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

        var account = await _accountRepository.GetByEmailPasswordAsync(email, password);
        if (account is null) ViewBag.LoginError = "Username or Password is wrong. Please control it";

        HttpContext.Session.SetString("UserSession", account.Id);
        return RedirectToAction("Attack");
    }

    [HttpGet]
    public async Task<IActionResult> Attack()
    {
        var userId = HttpContext.Session.GetString("UserSession");
        if (userId is null)
            return RedirectToAction("Login");

        var accountEntity = await _accountRepository.GetByIdAsync(userId);
        var financialEntity = await _financialRepository.GetByUserId(Convert.ToInt32(userId));

        if (accountEntity is null || financialEntity is null) return RedirectToAction("Login");

        var viewEntity = new AccountFinancialViewEntity
        {
            Account = accountEntity,
            Financial = financialEntity
        };

        return View(viewEntity);
    }

    [HttpPost]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Attack(IFormCollection form)
    {
        var userId = HttpContext.Session.GetString("UserSession");
        if (userId is null)
            return RedirectToAction("Attack");
        var accountEntity = await _accountRepository.GetByIdAsync(userId);
        var financialEntity = await _financialRepository.GetByUserId(Convert.ToInt32(userId));

        var customerNumber = form["customerNumber"].ToString();
        var money = form["money"].ToString();

        // customerNumber and money check
        if (string.IsNullOrEmpty(customerNumber) || string.IsNullOrEmpty(money))
        {
            ViewBag.moneyTransferError = "Please fill Customer number or Money field.";
            return View(null);
        }

        // money seize check check
        if (Convert.ToInt32(money) > financialEntity.Money)
        {
            ViewBag.moneyTransferError = "You have no enough money to send. Please control.";
            return View(null);
        }

        // destinatioun user null check
        var destinationAccountEntity = await _accountRepository.GetByIdAsync(customerNumber);
        if (destinationAccountEntity is null)
        {
            ViewBag.moneyTransferError = "There is no receiver customer Id you entered. Please control it";
            return View(null);
        }
        // destinatioun user financial null check
        var destinationfinancialEntity = await _financialRepository.GetByUserId(Convert.ToInt32(customerNumber));
        if (destinationfinancialEntity is null)
        {
            ViewBag.moneyTransferError = "There is a problem while getting destionation user financial information";
            return View(null);
        }

        // money change
        financialEntity.Money -= Convert.ToInt32(money);
        destinationfinancialEntity.Money += Convert.ToInt32(money);

        // update
        await _financialRepository.UpdateAsync(financialEntity);
        await _financialRepository.UpdateAsync(destinationfinancialEntity);


        var viewEntity = new AccountFinancialViewEntity
        {
            Account = accountEntity,
            Financial = financialEntity
        };

        
        return View(viewEntity);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}