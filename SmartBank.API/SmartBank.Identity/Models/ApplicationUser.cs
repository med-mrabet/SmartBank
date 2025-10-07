using Microsoft.AspNetCore.Identity;
using SmartBank.Domain.Entities;


namespace SmartBank.Identity.Models
{
    public class ApplicationUser :IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Account> Accounts { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Transaction> FromAccounts { get; set; }
        public ICollection<Transaction> ToAccounts { get; set; }

    }
}
