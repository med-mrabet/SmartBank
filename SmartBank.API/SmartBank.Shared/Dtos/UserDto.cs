namespace SmartBank.Shared.Dtos
{
    public class UserDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } 
        public string Email { get; set; } = string.Empty;
    }
}
