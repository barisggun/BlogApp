using BlogApp.Services.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Web.Areas.Admin.Controllers;
[Area("Admin")]
public class HomeController : Controller
{
    private readonly IArticleService _articleService;
    
    public HomeController(IArticleService articleService)
    {
        _articleService = articleService;
    }
    public async Task<IActionResult> Index()
    {
        var articles = await _articleService.GetAllArticlesAsync();
        return View(articles);
    }
}