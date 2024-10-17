using Microsoft.AspNetCore.Mvc;

namespace file_upload_basic.Mvc.Controllers;

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
        ViewBag.Message = "";
        return View();
    }

    [HttpPost]
    public IActionResult Attack(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            ViewBag.Message = "Lütfen bir dosya seçin.";
            return View();
        }

        var fileName = Path.GetFileName(file.FileName);
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyToAsync(stream);
        }

        ViewBag.Message = $"Dosya başarıyla yüklendi: {fileName}";
        return View();
    }
}