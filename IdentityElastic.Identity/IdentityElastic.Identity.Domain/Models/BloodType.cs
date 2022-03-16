namespace IdentityElastic.Identity.Domain.Models
{
    public class BloodType
    {
        public Guid Id { get; set; }
        public string BloodTypeName { get; set; } = default!;
        public int AmmountOfBloodInBank { get; set; } = 0;

    }
}
