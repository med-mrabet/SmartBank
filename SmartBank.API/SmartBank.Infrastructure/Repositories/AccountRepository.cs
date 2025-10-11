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
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly BankSmartContext bankSmartContext;
        public AccountRepository(BankSmartContext bankSmartContext) : base(bankSmartContext) 
        {
            this.bankSmartContext = bankSmartContext;
        }


        
    }
}
