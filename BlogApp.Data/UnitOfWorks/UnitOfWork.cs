using BlogApp.Core.Entities;
using BlogApp.Data.Context;
using BlogApp.Data.Repositories.Abstractions;
using BlogApp.Data.Repositories.Concretes;

namespace BlogApp.Data.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async ValueTask DisposeAsync()
    {
        await _dbContext.DisposeAsync();
    }

    public IRepository<T> GetRepository<T>() where T : class, IEntityBase, new()
    {
        return new Repository<T>(_dbContext);
    }

    public async Task<int> SaveAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public int Save()
    {
        return _dbContext.SaveChanges();
    }
}