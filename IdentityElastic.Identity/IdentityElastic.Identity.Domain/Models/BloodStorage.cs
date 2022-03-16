namespace IdentityElastic.Identity.Domain.Models
{
    public class BloodStorage
    {
        public Guid Id { get; set; }
        public int? ForeignBloodUnitId { get; set; }
        public string? BloodUnitLocation { get; set; }
        public bool IsAvailable { get; set; } = true;
        public bool IsAfterCovid { get; set; } = true;
        public virtual Guid BloodTypeId { get; set; }
        public virtual BloodType BloodType { get; set; }
        public virtual Guid DonationId { get; set; }
        public virtual Donation Donation { get; set; }
    }
}
