using BlogApp.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Data.Mappings;

public class UserRoleMap : IEntityTypeConfiguration<AppUserRole>
{
    public void Configure(EntityTypeBuilder<AppUserRole> b)
    {
        // Primary key
        b.HasKey(r => new { r.UserId, r.RoleId });

        // Maps to the AspNetUserRoles table
        b.ToTable("AspNetUserRoles");
    }
}