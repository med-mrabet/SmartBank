using SmartBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBank.Application.Persistence
{
    public interface IAccountRepository :IGenericRepository<Account>
    {
    }
}
