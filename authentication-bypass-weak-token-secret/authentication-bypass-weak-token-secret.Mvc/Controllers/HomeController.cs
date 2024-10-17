using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace authentication_bypass_weak_token_secret.Mvc.Controllers;

public class HomeController : Controller
{
    private const string SecretKey = "AEYjGNIRVGEtKSIarg0zCMEzOoNsKbxzzAFjTZWCrNfRaKHrOZ0gYf66cqRDcYrKFtv9Hp6J8NU3kh7xb47V4JvTGKGARAMhngqfcn7T63W7iCyvolcoaqRIw0Vi1aarol8902r"; // Bu, JwtSecurityKey oluşturmak için kullanılır

    
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
    public IActionResult Attack(IFormCollection form)
    {
        
        var username = form["username"].ToString();
        var password = form["password"].ToString();

        if (username == "user" && password == "pass")
        {
            var token = GenerateTokenWithoutSignature(username);
            HttpContext.Response.Headers["Authorization"] = "Bearer " + token;
            ViewBag.role = "User";
            return Redirect(nameof(Result));
        }
        return View();
    }
    
    [HttpGet]
    [Authorize]
    public IActionResult Result()
    {
        var token = Request.Headers.Authorization.ToString().Split("Bearer ")[1];
        if (string.IsNullOrEmpty(token))
        {
            return View();
        }

        ViewBag.role = GetRoleFromToken(token);
        return View();
        
    }
    
    private string GenerateTokenWithoutSignature(string username)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "User") 
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "yourIssuer",
            audience: "yourAudience",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
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