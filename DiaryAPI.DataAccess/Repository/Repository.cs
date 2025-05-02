using System.Linq.Expressions;
using DiaryAPI.DataAccess.Contexts;
using DiaryAPI.Entities.Entities;
using DiaryAPI.Entities.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DiaryAPI.DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly DiaryAPIDbContext _dbContext;
    public Repository(DiaryAPIDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public DbSet<T> Table => _dbContext.Set<T>();

    public IQueryable<T>? GetAll()
    {
        return Table.AsQueryable();
    }

    public IQueryable<T?> GetWhere(Expression<Func<T, bool>> method)
    {
        return Table.Where(method);
    }

    public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> method)
    {
        return await Table.FirstOrDefaultAsync(method);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await Table.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<bool> AddAsync(T entity)
    {
        await Table.AddAsync(entity);
        return true;
    }

    public bool Delete(T entity)
    {
        Table.Remove(entity);
        return true;
    }

    public async Task<int> SaveAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}