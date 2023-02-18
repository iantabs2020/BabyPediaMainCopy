using BabyPedia.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BabyPedia.Data;

public class BabyPediaContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Appointment> Appointments { get; set; }

    public DbSet<AppointmentPayment> AppointmentPayments { get; set; }

    public DbSet<AppointmentType> AppointmentTypes { get; set; }

    public DbSet<Child> Children { get; set; }

    public DbSet<HealthcareProfessional> HealthcareProfessionals { get; set; }

    public DbSet<ImmunizationRecord> ImmunizationRecords { get; set; }

    public DbSet<Log> Logs { get; set; }

    public DbSet<Parent> Parents { get; set; }

    public DbSet<PartneredPedia> PartneredPedias { get; set; }

    public DbSet<User> SiteUsers { get; set; }

    public DbSet<Vaccine> Vaccines { get; set; }

    public DbSet<VaccineOffered> VaccinesOffered { get; set; }

    public BabyPediaContext(DbContextOptions<BabyPediaContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Parent>()
            .HasMany<Appointment>();

        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}