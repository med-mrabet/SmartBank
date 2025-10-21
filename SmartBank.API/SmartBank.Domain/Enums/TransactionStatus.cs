using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBank.Domain.Enums
{
    public enum TransactionStatus
    {
       PENDING = 0,
       COMPLETED = 1,
       CANCELED = 2,
       FAILED = 3,
       REJECTED = 4
    }
}
