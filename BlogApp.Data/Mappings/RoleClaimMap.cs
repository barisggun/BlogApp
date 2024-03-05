using BlogApp.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Data.Mappings;

public class RoleClaimMap : IEntityTypeConfiguration<AppRoleClaim>
{
    public void Configure(EntityTypeBuilder<AppRoleClaim> b)
    {
        // Primary key
        b.HasKey(uc => uc.Id);

        // Maps to the AspNetUserClaims table
        b.ToTable("AspNetUserClaims");
    }
}