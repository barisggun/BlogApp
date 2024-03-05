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

        b.HasData(new AppUserRole
        {
            UserId = Guid.Parse("AE1143B6-1D26-4794-A589-B898AB3EC39F"),
            RoleId = Guid.Parse("04E3811B-601C-44A4-8FC8-AB675A5B8688")
        },
            new AppUserRole
            {
                UserId =Guid.Parse("1D8E5F54-B639-4F1A-A824-DB9192BAE014"),
                RoleId = Guid.Parse("CA6E803E-6C79-4C23-BE94-52183C688188")
            });
    }
}