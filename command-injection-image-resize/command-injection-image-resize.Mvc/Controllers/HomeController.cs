using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace command_injection_image_resize.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    
    private readonly ILogger<HomeController> _logger;

    public HomeController(IWebHostEnvironment webHostEnvironment,ILogger<HomeController> logger)
    {
        _webHostEnvironment = webHostEnvironment;
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
        ViewBag.status = false;
        ViewBag.result = "";
        
        return View();
    }

    [HttpPost]
    public IActionResult Attack(IFormCollection form)
    {
        var width = form["width"].ToString();
        var height = form["height"].ToString();
        var filename = form["filename"].ToString();
        
        var result = ResizeImage(width,height);
        ViewBag.status = result.Item1;
        ViewBag.result = result.Item2;
        
        
        return View();
    }

    public (bool,string) ResizeImage(string width, string height)
    {
        try
        {
            var imagesFolderPath = _webHostEnvironment.ContentRootPath + "/wwwroot/images/";
            var imageName = "Saka.jpg";
            var imagesPath = Path.Combine(imagesFolderPath, imageName);
            
            
            // ImageMagick 'convert' komutu kullanılıyor
            var resizeCommand = "convert";
            var outputFileName =  imagesFolderPath + $"resized_{width}{height}{imageName}";

            var processStartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // Komut parametreleri
            var command = $"-c \"{resizeCommand} '{imagesPath}' -resize {width}x{height}! '{outputFileName}'\"";
            processStartInfo.Arguments = command;

            // Process başlatılıyor
            using var process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();


            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            var response = "";
            if (process.ExitCode == 0)
            {
                response = $"Resim başarıyla boyutlandırıldı: {outputFileName}</br> Command Output: {output}";
                return (true, response);
            }
            
            response = $"Resim boyutlandırılırken hata ile karşılaşıldı.: {error}";
            return (true, response);
        }
        catch (Exception ex)
        {
            // Hata durumunda hata mesajını döndür
            return (false, $"Resim boyutlandırılırken hata ile karşılaşıldı.: {ex.Message}");
        }
    }
}