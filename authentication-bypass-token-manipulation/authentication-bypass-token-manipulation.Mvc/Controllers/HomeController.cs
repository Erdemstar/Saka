using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace authentication_bypass_token_manipulation.Mvc.Controllers;

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
        
        var token = HttpContext.Request.Cookies["token"];
        if (string.IsNullOrEmpty(token))
        {
            ViewBag.role = "";
            return View();
        }

        var tokenRole = GetRoleFromToken(token);
        ViewBag.role = tokenRole;
        return View();
    }
    
    [HttpPost]
    public IActionResult Attack(IFormCollection form)
    {
        
        var username = form["username"].ToString();
        var password = form["password"].ToString();

        if (username == "user" && password == "pass")
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(1), // Çerezin süresi
                HttpOnly = true, // Çerez yalnızca HTTP üzerinden erişilebilir
                Secure = true // Çerez yalnızca HTTPS üzerinden gönderilir
            };

            var token = GenerateTokenWithoutSignature(username);
            
            HttpContext.Response.Cookies.Append("token", token, cookieOptions);
            ViewBag.role = "user";
        }
        return View();
    }
    
    private string GenerateTokenWithoutSignature(string username)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "user") // Başlangıçta kullanıcı rolü atanıyor
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_keyyour_secret_keyyour_secret_key"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "http://localhost:8888",
            audience: "http://localhost:8888",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public string GetRoleFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        // Token'ı doğrulama işlemi yapmadan sadece çözümleme yapar
        var jwtToken = tokenHandler.ReadJwtToken(token);

        // Token içindeki role claim'ini bulma
        var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

        return roleClaim?.Value;
    }
    
}