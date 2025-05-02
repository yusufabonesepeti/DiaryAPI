using System.Linq.Expressions;
using DiaryAPI.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiaryAPI.Entities.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    DbSet<T> Table { get; }
    IQueryable<T>? GetAll();
    IQueryable<T?> GetWhere(Expression<Func<T, bool>> method);
    Task<T?> GetSingleAsync(Expression<Func<T, bool>> method);
    Task<T?> GetByIdAsync(Guid id);
    Task<bool> AddAsync(T entity);
    bool Delete(T entity);
    Task<int> SaveAsync();
}