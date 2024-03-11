using System.Collections;
using BlogApp.Entity.DTOs.Users;

namespace BlogApp.Entity.DTOs.Articles;

public class ArticleDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public CategoryDto Category { get; set; }
    public int ViewCount { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public bool IsDeleted { get; set; }

}