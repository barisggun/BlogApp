using System.Linq.Expressions;
using BlogApp.Core.Entities;
using BlogApp.Data.Context;
using BlogApp.Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Repositories.Concretes;

public class Repository<T> : IRepository<T> where T : class, IEntityBase, new()
{
    private readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    private DbSet<T> Table
    {
        get => _dbContext.Set<T>();
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T,bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = Table;
        if(predicate != null)
            query = query.Where(predicate);

        if(includeProperties.Any())
            foreach (var item in includeProperties)
                query = query.Include(item);

        return await query.ToListAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = Table;
        query = query.Where(predicate);
        
        if(includeProperties.Any())
            foreach (var item in includeProperties)
                query = query.Include(item);
        
        return await query.SingleOrDefaultAsync(); //FirstOrDefaultta bir liste varsa 10 tane değerden 1 tanesini alır, singleordefaultu kullanırsak; burada yüzde yüz o değerin gelmesi gerekir. Herhangi bir değer yoksa hata alırız.
    }

    public async Task<T> GetByGuidAsync(Guid id)
    {
        return await Table.FindAsync(id);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        await Task.Run(() => Table.Update(entity));
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        await Task.Run(() => Table.Remove(entity));
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await Table.AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
    {
        return await Table.CountAsync(predicate);
    }
    
    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
    }
}