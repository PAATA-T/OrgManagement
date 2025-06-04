using Microsoft.EntityFrameworkCore;
using OrgManagement.Entities.Models;

namespace OrgManagement.DataServices.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Person> Persons { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Organization configuration
            modelBuilder.Entity<Organization>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200);

                // Self-referencing relationship for sub organizations
                entity.HasMany(e => e.SubOrganizations)
                    .WithOne(e => e.ParentOrganization)
                    .HasForeignKey(e => e.ParentOrganizationId)
                    .OnDelete(DeleteBehavior.Restrict); // Avoid cascade delete loops

                // Navigation property for employees (persons)
                entity.HasMany(e => e.Employees)
                    .WithOne(p => p.Organization)
                    .HasForeignKey(p => p.OrganizationId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Person configuration
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.PersonalNumber)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsFixedLength(true);

                entity.Property(e => e.BirthDate)
                    .IsRequired();

                entity.Property(e => e.PhotoUrl)
                    .HasMaxLength(500); // optionally set max length or remove this if not needed

                entity.Property(e => e.ForeignLanguage)
                    .IsRequired();
            });
        }
}