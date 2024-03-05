using BlogApp.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Data.Mappings;

public class UserClaimMap : IEntityTypeConfiguration<AppUserClaim>
{
    public void Configure(EntityTypeBuilder<AppUserClaim> b)
    {
        // Primary key
        b.HasKey(uc => uc.Id);

        // Maps to the AspNetUserClaims table
        b.ToTable("AspNetUserClaims");
    }
}