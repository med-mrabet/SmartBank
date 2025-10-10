namespace SmartBank.Shared.Dtos
{
    public class AuthResponse
    {
       public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
