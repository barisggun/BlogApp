using BlogApp.Entity.DTOs.Users;

namespace BlogApp.Services.Services.Abstractions;

public interface ICategoryService
{
    public Task<List<CategoryDto>> GetAllCategoriesNonDeleted();
}