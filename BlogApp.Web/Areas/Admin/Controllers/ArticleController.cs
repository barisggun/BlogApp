using AutoMapper;
using BlogApp.Data.UnitOfWorks;
using BlogApp.Entity.DTOs.Articles;
using BlogApp.Entity.Entities;
using BlogApp.Services.Services.Abstractions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ArticleController : Controller
{
    private readonly IArticleService _articleService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    private readonly IValidator<Article> _validator;

    public ArticleController(IArticleService articleService, ICategoryService categoryService, IMapper mapper,
        IValidator<Article> validator)
    {
        _articleService = articleService;
        _categoryService = categoryService;
        _mapper = mapper;
        _validator = validator;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var articles = await _articleService.GetAllArticlesWithCategoryNonDeletedAsync();
        return View(articles);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var categories = await _categoryService.GetAllCategoriesNonDeleted();
        return View(new ArticleAddDto { Categories = categories });
    }

    [HttpPost]
    public async Task<IActionResult> Add(ArticleAddDto articleAddDto)
    {
        var map = _mapper.Map<Article>(articleAddDto);

        var result = await _validator.ValidateAsync(map);

        if (!result.IsValid)
        {
            result.AddToModelState(this.ModelState);
            var categories = await _categoryService.GetAllCategoriesNonDeleted();
            return View(new ArticleAddDto { Categories = categories });
        }
        else
        {
            await _articleService.CreateArticleAsync(articleAddDto);
            return RedirectToAction("Index", "Article", new { Area = "Admin" });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid articleId)
    {
        var article = await _articleService.GetArticleWithCategoryNonDeletedAsync(articleId);

        var categories = await _categoryService.GetAllCategoriesNonDeleted();

        var articleUpdateDto = _mapper.Map<ArticleUpdateDto>(article);
        articleUpdateDto.Categories = categories;

        return View(articleUpdateDto);
    }

    [HttpPost]
    public async Task<IActionResult> Update(ArticleUpdateDto articleUpdateDto)
    {
        var map = _mapper.Map<Article>(articleUpdateDto);

        var result = await _validator.ValidateAsync(map);

        if (result.IsValid)
        {
            await _articleService.UpdateArticleAsync(articleUpdateDto);
            return RedirectToAction("Index","Article",new {Area = "Admin"});
        }
        else
        {
            result.AddToModelState(this.ModelState);
        }

        var categories = await _categoryService.GetAllCategoriesNonDeleted();
        articleUpdateDto.Categories = categories;
        return View(articleUpdateDto);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid articleId)
    {
        await _articleService.SafeDeleteArticleAsync(articleId);

        return RedirectToAction("Index", "Article", new { Area = "Admin" });
    }
}