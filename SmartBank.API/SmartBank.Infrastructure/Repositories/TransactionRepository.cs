using SmartBank.Application.Persistence;
using SmartBank.Domain.Entities;
using SmartBank.Infrastructure.Context;
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


        
    }
}
