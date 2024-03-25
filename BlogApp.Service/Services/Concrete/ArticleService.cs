using System.Security.Claims;
using AutoMapper;
using BlogApp.Data.UnitOfWorks;
using BlogApp.Entity.DTOs.Articles;
using BlogApp.Entity.Entities;
using BlogApp.Services.Extensions;
using BlogApp.Services.Services.Abstractions;
using Microsoft.AspNetCore.Http;

namespace BlogApp.Services.Services.Concrete;

public class ArticleService : IArticleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ClaimsPrincipal _user;

    public ArticleService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _user = httpContextAccessor.HttpContext.User;
    }
    
    public async Task<List<ArticleDto>> GetAllArticlesWithCategoryNonDeletedAsync()
    {
        var articles =  await _unitOfWork.GetRepository<Article>().GetAllAsync(x=> !x.IsDeleted,x=>x.Category);
        
        var map = _mapper.Map<List<ArticleDto>>(articles);

        return map;
    }

    public async Task<ArticleDto> GetArticleWithCategoryNonDeletedAsync(Guid articleId)
    {
        var article =  await _unitOfWork.GetRepository<Article>().GetAsync(x=> !x.IsDeleted && x.Id == articleId, x=>x.Category);
        
        var map = _mapper.Map<ArticleDto>(article);

        return map;
    }
    
    public async Task CreateArticleAsync(ArticleAddDto articleAddDto)
    {
        //var userId = Guid.Parse("AE1143B6-1D26-4794-A589-B898AB3EC39F");
        
        // Claims uzun yol : var userId = _httpContextAccessor.HttpContext.User.GetLoggedInUserId();

        var userId = _user.GetLoggedInUserId();
        var userEmail = _user.GetLoggedInEmail();
        
        var imageId = Guid.Parse("F71F4B9A-AA60-461D-B398-DE31001BF214");
        // var article = new Article
        // {
        //     UserId = userId,
        //     Title = articleAddDto.Title,
        //     Content = articleAddDto.Content,
        //     CategoryId = articleAddDto.CategoryId
        // };

        var article = new Article(articleAddDto.Title, articleAddDto.Content, userId, userEmail, articleAddDto.CategoryId, imageId);
        
        await _unitOfWork.GetRepository<Article>().AddAsync(article);
        await _unitOfWork.SaveAsync();
    }

    public async Task<string> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto)
    {
        var userEmail = _user.GetLoggedInEmail();
        
        var article = await _unitOfWork.GetRepository<Article>()
            .GetAsync(x => !x.IsDeleted && x.Id == articleUpdateDto.Id, x => x.Category);
        
        article.Title = articleUpdateDto.Title;
        article.Content = articleUpdateDto.Content;
        article.CategoryId = articleUpdateDto.CategoryId;
        article.ModifiedDate = DateTime.UtcNow;
        article.ModifiedBy = userEmail;
        
        await _unitOfWork.GetRepository<Article>().UpdateAsync(article);

        await _unitOfWork.SaveAsync();

        return article.Title;
    }

    public async Task<string> SafeDeleteArticleAsync(Guid articleId)
    {
        var userEmail = _user.GetLoggedInEmail();
        
        var article = await _unitOfWork.GetRepository<Article>().GetByGuidAsync(articleId);

        article.IsDeleted = true;
        article.DeletedDate = DateTime.UtcNow;
        article.DeletedBy = userEmail;

        await _unitOfWork.GetRepository<Article>().UpdateAsync(article);
        await _unitOfWork.SaveAsync();

        return article.Title;
    }
}