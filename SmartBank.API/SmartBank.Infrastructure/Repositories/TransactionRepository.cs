using Microsoft.EntityFrameworkCore;
using SmartBank.Application.Persistence;
using SmartBank.Domain.Entities;
using SmartBank.Infrastructure.Context;
using SmartBank.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBank.Infrastructure.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly BankSmartContext bankSmartContext;
        public TransactionRepository(BankSmartContext bankSmartContext) : base(bankSmartContext) 
        {
            this.bankSmartContext = bankSmartContext;
        }

        public async Task<IEnumerable<Transaction>> GetUserTransactionsByAccountIdAsync(Guid accountId)
        {
            var res = new List<Transaction>();
            try
            {
                res = await bankSmartContext.Transactions.Include(p => p.FromAccount)
                .Include(p => p.ToAccount).Where(p => p.FromAccountId == accountId).ToListAsync();
            }
            catch (Exception ex) { throw; }
            return res;
        }
    }
}
