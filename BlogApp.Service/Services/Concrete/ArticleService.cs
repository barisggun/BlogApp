using BlogApp.Data.UnitOfWorks;
using BlogApp.Entity.Entities;
using BlogApp.Services.Services.Abstractions;

namespace BlogApp.Services.Services.Concrete;

public class ArticleService : IArticleService
{
    private readonly IUnitOfWork _unitOfWork;

    public ArticleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<List<Article>> GetAllArticlesAsync()
    {
        return await _unitOfWork.GetRepository<Article>().GetAllAsync();
    }
}