using SmartBank.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBank.Shared.Dtos
{
    public class AddTransactionDto
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public TransactionStatusDto Status { get; set; }
        public TransactionTypeDto TransactionType { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public Guid FromAccountId { get; set; }
        public Guid? ToAccountId { get; set; }
    }
}
