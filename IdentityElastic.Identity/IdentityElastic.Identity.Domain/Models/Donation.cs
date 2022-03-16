namespace IdentityElastic.Identity.Domain.Models
{
    public class Donation
    {
        public Guid Id { get; set; }
        public DateTime DonationDate { get; set; }
        public string? RejectionReason { get; set; }
        public virtual Guid BloodDonatorId { get; set; }
        public virtual BloodDonator BloodDonator { get; set; }
        public virtual BloodStorage BloodStorage { get; set; }
        public virtual ResultOfExamination ResultOfExamination { get; set; }
    }
}
