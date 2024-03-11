using AutoMapper;
using BlogApp.Data.UnitOfWorks;
using BlogApp.Entity.DTOs.Articles;
using BlogApp.Entity.Entities;
using BlogApp.Services.Services.Abstractions;

namespace BlogApp.Services.Services.Concrete;

public class ArticleService : IArticleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<List<ArticleDto>> GetAllArticlesWithCategoryNonDeletedAsync()
    {
        var articles =  await _unitOfWork.GetRepository<Article>().GetAllAsync(x=> !x.IsDeleted,x=>x.Category);
        
        var map = _mapper.Map<List<ArticleDto>>(articles);

        return map;
    }

    public async Task CreateArticleAsync(ArticleAddDto articleAddDto)
    {
        var userId = Guid.Parse("AE1143B6-1D26-4794-A589-B898AB3EC39F");

        var article = new Article
        {
            UserId = userId,
            Title = articleAddDto.Title,
            Content = articleAddDto.Content,
            CategoryId = articleAddDto.CategoryId
        };

        await _unitOfWork.GetRepository<Article>().AddAsync(article);
        await _unitOfWork.SaveAsync();
    }
}