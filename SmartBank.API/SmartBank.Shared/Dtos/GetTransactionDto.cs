namespace SmartBank.Shared.Dtos
{
    public class GetTransactionDto
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public TransactionStatusDto Status { get; set; }
        public TransactionTypeDto TransactionType { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public Guid FromAccountId { get; set; }
        public AccountDto FromAccount { get; set; }
        public Guid? ToAccountId { get; set; }
        public AccountDto ToAccount { get; set; }

    }
}
