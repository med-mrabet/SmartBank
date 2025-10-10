using Microsoft.AspNetCore.Identity;



namespace SmartBank.Shared.Models
{
    public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public Guid ApplicationRoleId { get; set; }

        public virtual ApplicationRole Role { get; set; }
    }
}
