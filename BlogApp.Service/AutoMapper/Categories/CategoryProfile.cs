using AutoMapper;
using BlogApp.Entity.DTOs.Users;
using BlogApp.Entity.Entities;

namespace BlogApp.Services.AutoMapper.Categories;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryDto, Category>().ReverseMap();
    }
}