using BlogApp.Entity.DTOs.Articles;

namespace BlogApp.Services.Services.Abstractions;

public interface IArticleService
{
    Task<List<ArticleDto>> GetAllArticlesWithCategoryNonDeletedAsync();
    Task<ArticleDto> GetArticleWithCategoryNonDeletedAsync(Guid articleId);
    Task<string> UpdateArticleAsync(ArticleUpdateDto articleUpdateDto);
    Task CreateArticleAsync(ArticleAddDto articleAddDto);
    Task<string> SafeDeleteArticleAsync(Guid articleId);
}