using BlogApp.Core.Entities;
using BlogApp.Data.Repositories.Abstractions;

namespace BlogApp.Data.UnitOfWorks;

public interface IUnitOfWork : IAsyncDisposable
{
    IRepository<T> GetRepository<T>() where T : class, IEntityBase, new();
    Task<int> SaveAsync();
    int Save();
}