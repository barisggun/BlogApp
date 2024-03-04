using BlogApp.Entity.Entities;

namespace BlogApp.Services.Services.Abstractions;

public interface IArticleService
{
    Task<List<Article>> GetAllArticlesAsync();
}