using Microsoft.AspNetCore.Mvc;
using sql_injection_profile.Core.Interface.Repository.Profile;

namespace sql_injection_profile.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProfileRepository _profileRepository;

    public HomeController(IProfileRepository profileRepository, ILogger<HomeController> logger)
    {
        _profileRepository = profileRepository;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Attack([FromQuery] string profileId)
    {
        var profile = await _profileRepository.GetByIdAsync(profileId);
        return View(profile);
    }
}