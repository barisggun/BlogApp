using BlogApp.Data.Context;
using BlogApp.Data.Repositories.Abstractions;
using BlogApp.Data.Repositories.Concretes;
using BlogApp.Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Data.Extensions;

public static class DataLayerExtensions
{
    public static IServiceCollection LoadDataLayerExtension(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        //IRepository'i çağırdığımda repository'den bir nesne oluşturulmasını gerektiğini scope ekleyerek belirtiyoruz. Scope'ın amacı da bu olacak. IRepository çağırdığımda repository döndürmüş olacak.

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
    
    
    
}