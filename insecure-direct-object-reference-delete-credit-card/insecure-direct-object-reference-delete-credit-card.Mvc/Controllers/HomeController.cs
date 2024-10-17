using insecure_direct_object_reference_delete_credit_card.Core.Entity.CreditCard;
using insecure_direct_object_reference_delete_credit_card.Core.Interface.Repository.Account;
using insecure_direct_object_reference_delete_credit_card.Core.Interface.Repository.CreditCard;
using Microsoft.AspNetCore.Mvc;

namespace insecure_direct_object_reference_delete_credit_card.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly IAccountRepository _accountRepository;
    private readonly ICreditCardRepository _creditCardRepository;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IAccountRepository accountRepository,
        ICreditCardRepository creditCardRepository)
    {
        _logger = logger;
        _accountRepository = accountRepository;
        _creditCardRepository = creditCardRepository;
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
        return Redirect("/Home/Profile/");
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var userId = HttpContext.Session.GetString("UserSession");
        if (userId is null) return RedirectToAction("Login");

        var account = await _accountRepository.GetByIdAsync(userId);
        if (account is null) return RedirectToAction("Login");

        return View(account);
    }

    [HttpGet]
    public async Task<IActionResult> Attack()
    {
        var userId = HttpContext.Session.GetString("UserSession");
        if (userId is null) return RedirectToAction("Login");

        var creditcard = await _creditCardRepository.GetByUserId(userId);

        return View(creditcard);
    }

    [HttpPost]
    public async Task<IActionResult> Attack(IFormCollection form)
    {
        var userId = HttpContext.Session.GetString("UserSession");
        if (userId is null) return RedirectToAction("Login");

        var type = form["type"].ToString();
        if (type == "") return RedirectToAction("Attack");

        if (type == "add")
        {
            var name = form["name"].ToString();
            var number = form["number"].ToString();
            var password = form["password"].ToString();
            var cve = form["cve"].ToString();

            await _creditCardRepository.AddAsync(new CreditCardEntity
            {
                UserId = userId,
                Name = name,
                Number = number,
                Password = password,
                CVE = cve
            });

            return RedirectToAction("Attack");
        }

        if (type == "delete")
        {
            var cardId = form["cardId"].ToString();

            var card = await _creditCardRepository.GetByIdAsync(cardId);

            if (card is null)
                // delete
                return RedirectToAction("Attack");

            await _creditCardRepository.DeleteAsync(card.Id);

            return RedirectToAction("Attack");
        }

        return RedirectToAction("Attack");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}