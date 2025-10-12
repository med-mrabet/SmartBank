
using SmartBank.Domain.Enums;
using SmartBank.Shared.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBank.Domain.Entities
{
    public class Account : BaseEntity.BaseEntity
    {
        public AccountType AccountType { get; set; } 
        public decimal Balance { get; set; } 
        public string Currency { get; set; } = string.Empty;
        [ForeignKey(nameof(UserId))]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

        public AccountStatus Status { get; set; }
        public string? ActionName { get; set; }

    }
}
