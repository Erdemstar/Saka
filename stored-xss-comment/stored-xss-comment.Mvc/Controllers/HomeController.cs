using Microsoft.AspNetCore.Mvc;
using stored_xss_input.Core.Entity.Comment;
using stored_xss_input.Core.Interface.Repository.Comment;

namespace stored_xss_input.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ICommentRepository _commentRepository;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ICommentRepository commentRepository, ILogger<HomeController> logger)
    {
        _commentRepository = commentRepository;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Attack()
    {
        var comments = await _commentRepository.GetAllAsync();
        return View(comments);
    }

    [HttpPost]
    public async Task<IActionResult> Attack(IFormCollection form)
    {
        var name = form["Name"].ToString();
        var com = form["Comment"].ToString();

        var comment = new CommentEntity
        {
            Name = name,
            Comment = com
        };

        await _commentRepository.AddAsync(comment);
        return RedirectToAction(nameof(Attack));
    }
}