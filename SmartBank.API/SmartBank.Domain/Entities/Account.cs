
namespace SmartBank.Domain.Entities
{
    public class Account : BaseEntity.BaseEntity
    {
        public string AccountType { get; set; } = string.Empty;
        public decimal Balance { get; set; } 
        public string Currency { get; set; } = string.Empty;
        public Guid UserId { get; set; }

    }
}
