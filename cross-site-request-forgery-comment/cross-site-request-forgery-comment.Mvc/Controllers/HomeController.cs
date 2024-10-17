using cross_site_request_forgery_comment.Core.Entity.Comment;
using cross_site_request_forgery_comment.Core.Entity.View.AccountComment;
using cross_site_request_forgery_comment.Core.Interface.Repository.Account;
using cross_site_request_forgery_comment.Core.Interface.Repository.Comment;
using Microsoft.AspNetCore.Mvc;

namespace cross_site_request_forgery_comment.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly IAccountRepository _accountRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IAccountRepository accountRepository, ICommentRepository commentRepository,
        ILogger<HomeController> logger)
    {
        _accountRepository = accountRepository;
        _commentRepository = commentRepository;
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
        if (userId is null) return RedirectToAction("Login");

        var accountCommentList = new List<AccountCommentViewEntity>();

        var comments = await _commentRepository.GetAllAsync();
        if (comments is null)
        {
            ViewBag.CommentError = "Username or Password is wrong. Please control it";
            return View(null);
        }

        foreach (var comment in comments)
        {
            var account = await _accountRepository.GetByIdAsync(comment.UserId);
            accountCommentList.Add(new AccountCommentViewEntity
            {
                AccountEntity = account,
                CommentEntity = comment
            });
        }

        return View(accountCommentList);
    }

    [HttpPost]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> Attack(IFormCollection form)
    {
        var userId = HttpContext.Session.GetString("UserSession");
        if (userId is null)
            return RedirectToAction("Login");

        var comment = form["comment"].ToString();

        if (string.IsNullOrEmpty(comment))
        {
            ViewBag.AttackError = "Comment is null. Please fill it and send again";
            return View(null);
        }
        
        var accountEntity = await _accountRepository.GetByIdAsync(userId);
        if (accountEntity is null)
        {
            ViewBag.AttackError = "There is a problem while getting current user account information";
            return View(null);
        }

        await _commentRepository.AddAsync(new CommentEntity()
        {
            Comment = comment,
            UserId = accountEntity.Id
        });

        
        return RedirectToAction("Attack");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}