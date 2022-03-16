using Microsoft.AspNetCore.Identity;

namespace IdentityElastic.Identity.Domain.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public virtual Guid? BloodDonatorId { get; set; }
        public virtual BloodDonator? BloodDonator { get; set; }
    }
}
