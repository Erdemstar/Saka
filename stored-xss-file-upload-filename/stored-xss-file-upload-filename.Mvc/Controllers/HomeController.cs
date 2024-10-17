using Microsoft.AspNetCore.Mvc;
using stored_xss_file_upload_filename.Core.Entity.DTO.Csv.Request;
using stored_xss_file_upload_filename.Core.Entity.DTO.Csv.Response;
using stored_xss_file_upload_filename.Core.Interface.Repository.Comment;
using stored_xss_file_upload2.Core.Entity.Comment;


namespace stored_xss_file_upload_filename.Mvc.Controllers;

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
        if (comments is not null)
        {
            var csvDataList = new List<ResponseCsvDTO>();
            foreach (var comment in comments)
                csvDataList.Add(new ResponseCsvDTO
                {
                    Filename = comment.Filename,
                    Name = comment.Name,
                    Comment = comment.Comment
                });

            return View(csvDataList);
        }

        return View(null);
    }

    [HttpPost]
    public async Task<IActionResult> Attack(IFormFile csvFile)
    {
        if (csvFile == null || csvFile.Length == 0) return RedirectToAction(nameof(Attack));

        var csvDataList = new List<RequestCsvDTO>();

        using (var reader = new StreamReader(csvFile.OpenReadStream()))
        {
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                var values = line.Split(',');

                var csvData = new RequestCsvDTO
                {
                    Name = values[0],
                    Comment = values[1]
                };
                csvDataList.Add(csvData);
            }
        }

        foreach (var csvData in csvDataList)
        {
            var comment = new CommentEntity
            {
                Name = csvData.Name,
                Comment = csvData.Comment,
                Filename = csvFile.FileName
            };

            await _commentRepository.AddAsync(comment);
        }

        return RedirectToAction(nameof(Attack));
    }
}
