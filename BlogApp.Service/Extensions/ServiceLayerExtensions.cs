using System.Reflection;
using BlogApp.Services.Services.Abstractions;
using BlogApp.Services.Services.Concrete;
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

        services.AddAutoMapper(assembly);
        return services;
    }

}