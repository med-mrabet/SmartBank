namespace SmartBank.Shared.Dtos
{
    public class GetAccountDto
    {
        public string AccountType { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public string Currency { get; set; } = string.Empty;
        public AccountStatusDto AccountStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid UserId { get; set; }
        public UserDto User { get; set; }
    }
}
