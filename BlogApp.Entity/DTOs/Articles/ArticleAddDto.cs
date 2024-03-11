using BlogApp.Entity.DTOs.Users;

namespace BlogApp.Entity.DTOs.Articles;

public class ArticleAddDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid CategoryId { get; set; }
    public IList<CategoryDto> Categories { get; set; }

}