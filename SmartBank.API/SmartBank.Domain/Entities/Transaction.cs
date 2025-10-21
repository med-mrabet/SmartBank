using SmartBank.Domain.Enums;
using SmartBank.Shared.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBank.Domain.Entities
{
    public class Transaction : BaseEntity.BaseEntity
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public TransactionStatus? Status { get; set; } 
        public TransactionType TransactionType { get; set; } 
        public string Description { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        [ForeignKey(nameof(FromAccountId))]

        public Guid FromAccountId { get; set; }
        public Account FromAccount { get; set; }
        [ForeignKey(nameof(ToAccountId))]
        public Guid? ToAccountId { get; set; }
        public Account ToAccount { get; set; }

    }
}
