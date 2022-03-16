namespace IdentityElastic.Identity.Domain.Models
{
    public class ResultOfExamination
    {
        public Guid Id { get; set; }
        public double HB { get; set; } = default;
        public double HT { get; set; } = default;
        public double RBC { get; set; } = default;
        public double WBC { get; set; } = default;
        public double PLT { get; set; } = default;
        public double MCH { get; set; } = default;
        public double MCHC { get; set; } = default;
        public double MCV { get; set; } = default;
        public double NE { get; set; } = default;
        public double EO { get; set; } = default;
        public double BA { get; set; } = default;
        public double LY { get; set; } = default;
        public double MO { get; set; } = default;
        public int BloodPressureUpper { get; set; } = default;
        public int BloodPressureLower { get; set; } = default;
        public int Height { get; set; } = default;
        public int Weight { get; set; } = default;
        public virtual Guid DonationId { get; set; }
        public virtual Donation Donation { get; set; }
    }
}
