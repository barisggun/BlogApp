using BlogApp.Services.Services.Abstractions;
using BlogApp.Services.Services.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Services.Extensions;

public static class ServiceLayerExtensions
{
    public static IServiceCollection ServiceLayerExtension(this IServiceCollection services)
    {
        services.AddScoped<IArticleService, ArticleService>();
        return services;
    }

}