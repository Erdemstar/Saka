using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace server_side_request_forgery_show_image.Mvc.Controllers;

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
    public async Task<IActionResult> Attack([FromQuery] string url)
    {
        if (string.IsNullOrEmpty(url))
            return View();

        var isValidUrl = IsValidUrl(url);
        if (isValidUrl == false)
        {
            ViewBag.Error = "Geçersiz URL, lütfen geçerli bir URL girin.";
            return View();
        }

        // Resmi alma işlemi
        var imageContent = await FetchExternalContent(url);

        if (imageContent is null) ViewBag.Error = "Resim alınırken hata oluştu.";

        return View(imageContent);
    }

    public bool IsValidUrl(string url)
    {
        if (Uri.TryCreate(url, UriKind.Absolute, out var uriResult))
            // İlk önce, URI scheme'lerinin HTTP veya HTTPS olduğundan emin ol
            if (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps ||
                uriResult.Scheme == Uri.UriSchemeFile)
            {
                // Localhost ve yerel IP adreslerini kontrol et
                var host = uriResult.Host;
                if (host.Equals("localhost", StringComparison.OrdinalIgnoreCase) ||
                    host.Equals("127.0.0.1") ||
                    host.Equals("::1"))
                    // Localhost erişimlerini engelle
                    return false;
                return true;
            }

        return false;
    }

    public async Task<HttpResponseEntity> FetchExternalContent(string url)
    {
        var uri = new Uri(url);

        // Eğer file şeması ise, dosya sisteminden oku
        if (uri.Scheme == Uri.UriSchemeFile)
        {
            var filePath = uri.LocalPath;

            // Dosyanın var olup olmadığını kontrol et
            if (System.IO.File.Exists(filePath))
            {
                var fileContent = await System.IO.File.ReadAllBytesAsync(filePath);
                return new HttpResponseEntity
                {
                    isSuccess = true,
                    content = Convert.ToBase64String(fileContent)
                };
            }

            return new HttpResponseEntity
            {
                isSuccess = false,
                content = ""
            };
        }

        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(url);
        var content = await response.Content.ReadAsByteArrayAsync();
        if (response.IsSuccessStatusCode)
            return new HttpResponseEntity
            {
                isSuccess = true,
                content = Convert.ToBase64String(content)
            };

        return new HttpResponseEntity
        {
            isSuccess = false,
            content = ""
        };
    }
}

public class HttpResponseEntity
{
    public bool isSuccess { get; set; }
    public string content { get; set; }
}