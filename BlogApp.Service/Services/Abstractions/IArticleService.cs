using BlogApp.Entity.DTOs.Articles;
using BlogApp.Entity.Entities;

namespace BlogApp.Services.Services.Abstractions;

public interface IArticleService
{
    Task<List<ArticleDto>> GetAllArticlesAsync();
}