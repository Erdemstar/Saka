using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

namespace authentication_bypass_cookie.Mvc.Controllers;

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
        ViewBag.result = "";
        return View();
    }
    
    [HttpPost]
    public IActionResult Attack(IFormCollection form)
    {
        
        var ip = form["ip"].ToString();
        ViewBag.result  = ExecutePing(ip);
        return View();
    }
    
    public string ExecutePing(string target)
    {
        try
        {
            // Ping komutu oluşturuluyor
            var pingCommand = "ping -c 4";

            // ProcessStartInfo ile komutu ayarlıyoruz
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                // Komutu ve parametreyi Process'e veriyoruz
                Arguments = $"-c {pingCommand} {target}"
            };

            // Process başlatılıyor
            using var process = new Process { StartInfo = processStartInfo };
            process.Start();

            // Çıktıyı al
            var result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            // Komut çıktılarını göster
            return result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}