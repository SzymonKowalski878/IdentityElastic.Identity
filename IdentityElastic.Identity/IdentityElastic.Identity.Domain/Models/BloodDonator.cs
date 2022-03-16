namespace IdentityElastic.Identity.Domain.Models
{
    public class BloodDonator
    {
        public Guid Id { get; set; }
        public string Pesel { get; set; } = default!;
        public string HomeAdress { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public int AmmountOfBloodDonated { get; set; } = 0;
        public virtual ApplicationUser User { get; set; }
        public virtual Guid BloodTypeId { get; set; }
        public virtual BloodType BloodType { get; set; }
    }
}
