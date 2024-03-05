using BlogApp.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Data.Mappings;

public class RoleMap : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> b)
    {
        // Primary key
        b.HasKey(r => r.Id);

        // Index for "normalized" role name to allow efficient lookups
        b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

        // Maps to the AspNetRoles table
        b.ToTable("AspNetRoles");

        // A concurrency token for use with the optimistic concurrency checking
        b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

        // Limit the size of columns to use efficient database types
        b.Property(u => u.Name).HasMaxLength(256);
        b.Property(u => u.NormalizedName).HasMaxLength(256);

        // The relationships between Role and other entity types
        // Note that these relationships are configured with no navigation properties

        // Each Role can have many entries in the UserRole join table
        b.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

        // Each Role can have many associated RoleClaims
        b.HasMany<AppRoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();

        b.HasData(new AppRole
        {
            Id = Guid.Parse("04E3811B-601C-44A4-8FC8-AB675A5B8688"),
            Name = "Superadmin",
            NormalizedName = "SUPERADMIN",
            ConcurrencyStamp = Guid.NewGuid().ToString()
        },
            new AppRole
            {
                Id = Guid.Parse("CA6E803E-6C79-4C23-BE94-52183C688188"),
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new AppRole
            {
                Id = Guid.Parse("34D186CE-404C-4E6B-A7BE-C5EFFA15D4BA"),
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });
    }
}