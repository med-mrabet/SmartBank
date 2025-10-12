using Microsoft.EntityFrameworkCore;
using SmartBank.Application.Persistence;
using SmartBank.Domain.Entities.BaseEntity;
using SmartBank.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartBank.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly BankSmartContext _context;

        public GenericRepository(BankSmartContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> ExistByCreterieAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AnyAsync(expression);

        }

        public async Task<List<T?>> FindByCreterieAsync(Expression<Func<T, bool>> expression)
        {
            var res=  _context.Set<T>().Where(expression);
            return await res.ToListAsync();


        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid? id)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}