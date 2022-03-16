using IdentityElastic.Identity.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityElastic.Identity.Infrastructure.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<BloodDonator> BloodDonators { get; set; } = default!;
        public DbSet<BloodType> BloodTypes { get; set; } = default!;
        public DbSet<Donation> Donations { get; set; } = default!;
        public DbSet<BloodStorage> BloodStorage { get; set; } = default!;
        public DbSet<ResultOfExamination> ResultsOfExaminations { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BloodDonator>(e =>
            {
                e.HasKey(e => e.Id);

                e.HasOne(m => m.User)
                .WithOne(m => m.BloodDonator)
                .HasForeignKey<ApplicationUser>(m => m.BloodDonatorId)
                .OnDelete(DeleteBehavior.NoAction);

                e.HasOne(m => m.BloodType)
                .WithMany()
                .HasForeignKey(m => m.BloodTypeId)
                .OnDelete(DeleteBehavior.NoAction);

                e.HasIndex(m => m.Pesel)
                .IsUnique();
            });

            builder.Entity<BloodStorage>(e =>
            {
                e.HasKey(e => e.Id);

                e.HasOne(m => m.BloodType)
                .WithMany()
                .HasForeignKey(m => m.BloodTypeId)
                .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<BloodType>(e =>
            {
                e.HasKey(e => e.Id);
            });

            builder.Entity<Donation>(e =>
            {
                e.HasKey(e => e.Id);

                e.HasOne(m => m.BloodDonator)
                .WithMany()
                .HasForeignKey(m => m.BloodDonatorId)
                .OnDelete(DeleteBehavior.NoAction);

                e.HasOne(m => m.ResultOfExamination)
                .WithOne(m => m.Donation)
                .HasForeignKey<ResultOfExamination>(m => m.DonationId)
                .OnDelete(DeleteBehavior.NoAction);

                e.HasOne(m => m.BloodStorage)
                .WithOne(m => m.Donation)
                .HasForeignKey<BloodStorage>(m => m.DonationId)
                .OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<ResultOfExamination>(e =>
            {
                e.HasKey(m => m.Id);
            });

            builder.Entity<ApplicationUser>(e =>
            {
                e.HasKey(e => e.Id);

                e.HasIndex(e => e.Email)
                .IsUnique();
            });
        }
    }
}
