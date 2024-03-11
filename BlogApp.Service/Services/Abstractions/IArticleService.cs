using BlogApp.Entity.DTOs.Articles;

namespace BlogApp.Services.Services.Abstractions;

public interface IArticleService
{
    Task<List<ArticleDto>> GetAllArticlesWithCategoryNonDeletedAsync();
    Task CreateArticleAsync(ArticleAddDto articleAddDto);
}