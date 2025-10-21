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
    public class AuditLogRepository : GenericRepository<AuditLog>, IAuditLogRepository
    {
        private readonly BankSmartContext _bankSmartContext;

        public AuditLogRepository(BankSmartContext bankSmartContext) : base(bankSmartContext)
        {
            _bankSmartContext = bankSmartContext;
        }

        public async Task BulkAddAsync(List<AuditLog> audits)
        {
            await _bankSmartContext.AuditLogs.AddRangeAsync(audits);
            await _bankSmartContext.SaveChangesAsync();
        }
    }
}
