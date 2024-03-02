using System.Collections.Immutable;
using System.Reflection;
using BlogApp.Data.Mappings;
using BlogApp.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Article> Articles { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new ArticleMap()); Bu bir tek tek tanımlama biçimidir.
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //Istedigimiz kadar mapping yazalım bunu kullandığımız her yerde teker teker tanımlamak yerine bu metotla tanımlayabiliyoruz. (IEntityTypeConfiguration)
        
        
    }
}