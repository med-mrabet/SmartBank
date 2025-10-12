namespace SmartBank.Shared.Dtos
{
    public class AccountDto
    {
        public AccountTypeDto AccountType { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; } = string.Empty;
        public AccountStatusDto Status { get; set; }

        public string? ActionName { get; set; }
    }
}
