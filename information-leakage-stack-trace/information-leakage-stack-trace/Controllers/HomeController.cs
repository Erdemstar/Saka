using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using information_leakage_stack_trace.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace information_leakage_stack_trace.Controllers;

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

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        // Hata sayfasında hata mesajını ve stack trace'i gösteriyoruz
        var exceptionDetail = HttpContext.Features.Get<IExceptionHandlerFeature>();

        return View(new ErrorViewModel 
        { 
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            ErrorMessage = exceptionDetail?.Error.Message,
            StackTrace = exceptionDetail?.Error.StackTrace
        });
    }
}