using SmartBank.Domain.Enums;

namespace SmartBank.Domain.Entities
{
    public class Transaction : BaseEntity.BaseEntity
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public TransactionStatus Status { get; set; } 
        public TransactionType TransactionType { get; set; } 
        public string Description { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }

        public Guid? FromAccountId { get; set; }
        public User? FromAccount { get; set; }

        public Guid? ToAccountId { get; set; }
        public User? ToAccount { get; set; }

    }
}
