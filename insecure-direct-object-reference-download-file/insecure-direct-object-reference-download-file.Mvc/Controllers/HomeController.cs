using insecure_direct_object_reference_download_file.Core.Interface.Repository.Account;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;

namespace insecure_direct_object_reference_download_file.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly IAccountRepository _accountRepository;
    private readonly ILogger<HomeController> _logger;

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

        var accountEntity = await _accountRepository.GetUserByEmailPasswordAsync(email, password);
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
    public async Task<IActionResult> Attack(string email)
    {
        var userId = HttpContext.Session.GetString("UserSession");
        if (userId is null) return RedirectToAction("Login");

        var account = await _accountRepository.GetByIdAsync(userId);
        if (account is null) return RedirectToAction("Login");

        var userAccount = await _accountRepository.GetUserByEmailAsync(email);
        if (userAccount is null) return RedirectToAction("Profile");

        using var stream = new MemoryStream();
        var pdfWriter = new PdfWriter(stream);
        var pdfDocument = new PdfDocument(pdfWriter);
        var document = new Document(pdfDocument);

        document.Add(new Paragraph("Kullanıcı Listesi").SetFontSize(20));
        var table = new Table(6);

        table.AddHeaderCell("Email");
        table.AddHeaderCell("Password");
        table.AddHeaderCell("Username");
        table.AddHeaderCell("Name");
        table.AddHeaderCell("Lastname");
        table.AddHeaderCell("Hobby");
        table.AddHeaderCell("Country");

        table.AddCell(userAccount.Email);
        table.AddCell(userAccount.Password);
        table.AddCell(userAccount.Name);
        table.AddCell(userAccount.Lastname);
        table.AddCell(userAccount.Email);
        table.AddCell(userAccount.Hobby);
        table.AddCell(userAccount.Address);

        document.Add(table);
        document.Close();
        return File(stream.ToArray(), "application/pdf", "download-profile.pdf");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}