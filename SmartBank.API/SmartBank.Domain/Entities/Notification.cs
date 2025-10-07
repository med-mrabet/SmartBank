using Microsoft.AspNet.Identity.EntityFramework;

namespace SmartBank.Domain.Entities
{
    public class Notification : BaseEntity.BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public Guid UserId { get; set; }

    }
}
