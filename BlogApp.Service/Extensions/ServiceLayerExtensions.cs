using System.Globalization;
using System.Reflection;
using BlogApp.Services.FluentValidations;
using BlogApp.Services.Services.Abstractions;
using BlogApp.Services.Services.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Services.Extensions;

public static class ServiceLayerExtensions
{
    public static IServiceCollection ServiceLayerExtension(this IServiceCollection services)
    {
        //automapper için
        var assembly = Assembly.GetExecutingAssembly();
        
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<ICategoryService, CategoryService>();

        services.AddAutoMapper(assembly);

        services.AddControllersWithViews().AddFluentValidation(opt =>
        {
            opt.RegisterValidatorsFromAssemblyContaining<ArticleValidator>();
            opt.DisableDataAnnotationsValidation = true;
            opt.ValidatorOptions.LanguageManager.Culture = new CultureInfo("tr");
            
        });
        return services;
    }

}