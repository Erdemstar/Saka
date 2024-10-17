using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace server_side_request_forgery_basic.Mvc.Controllers;

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
    [AllowAnonymous]
    public IActionResult Attack()
    {
        return View();
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Attack(IFormCollection form)
    {
        var url = form["url"].ToString();
        var isValidUrl = IsValidUrl(url);
        if (isValidUrl == false)
        {
            ViewBag.urlFormatError = "Please enter valid URL";
            return View();
        }
        ViewBag.urlFormatError = "";
        var response = await FetchExternalData(url);
        return View(response);
    }
    
    public bool IsValidUrl(string url)
    {
        if (Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult))
        {
            // İlk önce, URI scheme'lerinin HTTP veya HTTPS olduğundan emin ol
            if (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
            {
                // Localhost ve yerel IP adreslerini kontrol et
                var host = uriResult.Host;
                if (host.Equals("localhost", StringComparison.OrdinalIgnoreCase) || 
                    host.Equals("127.0.0.1") || 
                    host.Equals("::1"))
                {
                    // Localhost erişimlerini engelle
                    return false;
                }
                return true;
            }
        }
        return false;
    }
    public async Task<HttpResponseEntity> FetchExternalData(string targetUrl)
    {
        try
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(targetUrl);
            var content = await response.Content.ReadAsStringAsync();
            return new HttpResponseEntity()
            {
                url = targetUrl,
                isSuccess = true,
                statusCode = response.StatusCode.ToString(),
                content = content
            };
        }
        catch (HttpRequestException ex)
        {
            return new HttpResponseEntity()
            {
                url = targetUrl,
                isSuccess = false,
                statusCode = "0",
                content = ex.Message
            };
        }
    }
    
}

public class HttpResponseEntity
{
    public string url { get; set; }
    public bool isSuccess { get; set; }
    public string statusCode { get; set; }
    public string content { get; set; }
}