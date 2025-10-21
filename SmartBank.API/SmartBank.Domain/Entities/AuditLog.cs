using SmartBank.Shared.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBank.Domain.Entities
{
    public class AuditLog : BaseEntity.BaseEntity
    {
        public string EntityType { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string OldValue { get; set; } = string.Empty;
        public string NewValue { get; set; } = string.Empty;

        public bool IsRead { get; set; } = false;

        [ForeignKey(nameof(UserId))]
        public Guid? UserId { get; set; }
        public ApplicationUser? User { get; set; }

    }

    public class LogInHistory : BaseEntity.BaseEntity
    {
        public string? IpAddress { get; set; } = string.Empty;
        public string? Device { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;
        public bool IsSuccessful { get; set; }

        [ForeignKey(nameof(UserId))]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
