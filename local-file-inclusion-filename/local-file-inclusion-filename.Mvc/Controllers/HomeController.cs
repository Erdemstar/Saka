using local_file_inclusion_filename.Core.Entity.File;
using Microsoft.AspNetCore.Mvc;

namespace local_file_inclusion_filename_Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Attack(string filename)
    {
        if (string.IsNullOrEmpty(filename)) filename = "sample.txt";

        var fileEntity = new FileEntity
        {
            Name = "wwwroot/files/" + filename
        };

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileEntity.Name);

        try
        {
            fileEntity.Content = System.IO.File.Exists(filePath)
                ? System.IO.File.ReadAllText(filePath)
                : "File is not exist.";
        }
        catch (Exception ex)
        {
            fileEntity.Content = $"There is a problem while file reading: {ex.Message}";
        }

        return View(fileEntity);
    }
}