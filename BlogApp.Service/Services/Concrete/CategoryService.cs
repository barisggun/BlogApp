using AutoMapper;
using BlogApp.Data.UnitOfWorks;
using BlogApp.Entity.DTOs.Users;
using BlogApp.Entity.Entities;
using BlogApp.Services.Services.Abstractions;

namespace BlogApp.Services.Services.Concrete;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<List<CategoryDto>> GetAllCategoriesNonDeleted()
    {
        var categories = await _unitOfWork.GetRepository<Category>().GetAllAsync(x => !x.IsDeleted);
        var map = _mapper.Map<List<CategoryDto>>(categories);

        return map;
    }
}