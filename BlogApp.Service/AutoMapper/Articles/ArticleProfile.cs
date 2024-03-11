using AutoMapper;
using BlogApp.Entity.DTOs.Articles;
using BlogApp.Entity.Entities;

namespace BlogApp.Services.AutoMapper;

public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<ArticleDto, Article>().ReverseMap();
        CreateMap<ArticleUpdateDto, Article>().ReverseMap();
        CreateMap<ArticleUpdateDto, ArticleDto>().ReverseMap();
    }
}