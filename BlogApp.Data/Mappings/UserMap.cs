using BlogApp.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> b)
    {
        // Primary key
        b.HasKey(u => u.Id);

        // Indexes for "normalized" username and email, to allow efficient lookups
        b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
        b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

        // Maps to the AspNetUsers table
        b.ToTable("AspNetUsers");

        // A concurrency token for use with the optimistic concurrency checking
        b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

        // Limit the size of columns to use efficient database types
        b.Property(u => u.UserName).HasMaxLength(100);
        b.Property(u => u.NormalizedUserName).HasMaxLength(256);
        b.Property(u => u.Email).HasMaxLength(100);
        b.Property(u => u.NormalizedEmail).HasMaxLength(256);

        // The relationships between User and other entity types
        // Note that these relationships are configured with no navigation properties

        // Each User can have many UserClaims
        b.HasMany<AppUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

        // Each User can have many UserLogins
        b.HasMany<AppUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

        // Each User can have many UserTokens
        b.HasMany<AppUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

        // Each User can have many entries in the UserRole join table
        b.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

        var superAdmin =new AppUser
        {
            Id = Guid.Parse("AE1143B6-1D26-4794-A589-B898AB3EC39F"),
            UserName = "superadmin@gmail.com",
            NormalizedUserName = "SUPERADMIN@GMAIL.COM",
            Email = "superadmin@gmail.com",
            NormalizedEmail = "SUPERADMIN@GMAIL.COM",
            PhoneNumber = "+90543999999",
            FirstName = "Superadmin",
            LastName = "Gun",
            PhoneNumberConfirmed = true,
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        superAdmin.PasswordHash = CreatePasswordHash(superAdmin, "123456");
        
        var admin =new AppUser
        {
            Id = Guid.Parse("1D8E5F54-B639-4F1A-A824-DB9192BAE014"),
            UserName = "admin@gmail.com",
            NormalizedUserName = "ADMIN@GMAIL.COM",
            Email = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            PhoneNumber = "+90432425643",
            FirstName = "Admin",
            LastName = "Gun",
            PhoneNumberConfirmed = false,
            EmailConfirmed = false,
            SecurityStamp = Guid.NewGuid().ToString()
        };
        admin.PasswordHash = CreatePasswordHash(admin,"123456");

        b.HasData(superAdmin, admin);
    }
    private string CreatePasswordHash(AppUser user, string password)
    {
        var passwordHasher = new PasswordHasher<AppUser>();
        return passwordHasher.HashPassword(user, password);
    }
    }
    