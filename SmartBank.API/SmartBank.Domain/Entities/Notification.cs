using Microsoft.AspNet.Identity.EntityFramework;
using SmartBank.Shared.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBank.Domain.Entities
{
    public class Notification : BaseEntity.BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public bool IsRead { get; set; } = false;

        [ForeignKey(nameof(UserId))]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
