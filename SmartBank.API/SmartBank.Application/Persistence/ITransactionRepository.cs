using SmartBank.Domain.Entities;
using SmartBank.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBank.Application.Persistence
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        public Task<IEnumerable<Transaction>> GetUserTransactionsByAccountIdAsync(Guid accountId);
    }
}
