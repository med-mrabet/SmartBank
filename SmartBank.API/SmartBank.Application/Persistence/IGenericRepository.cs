
using SmartBank.Domain.Entities.BaseEntity;
using System.Linq.Expressions;

namespace SmartBank.Application.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid? id);
    Task<bool> ExistByCreterieAsync(Expression<Func<T, bool>> expression);
    Task<List<T?>> FindByCreterieAsync(Expression<Func<T, bool>> expression);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
