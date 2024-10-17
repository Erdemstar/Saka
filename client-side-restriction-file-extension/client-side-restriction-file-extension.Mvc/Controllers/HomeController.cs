using Microsoft.AspNetCore.Mvc;

namespace client_side_restriction_file_extension.Mvc.Controllers;

public class HomeController : Controller
{
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
    public IActionResult Attack()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Attack(IFormFile file)
    {
        ViewBag.status = file is null ? $"File is not empty." : $"{file.FileName} named file upload with success";
        return View();
    }
}