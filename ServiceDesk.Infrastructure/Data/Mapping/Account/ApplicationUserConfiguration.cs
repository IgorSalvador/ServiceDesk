using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Infrastructure.Data.Mapping.Account;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("ApplicationUser");

        builder.HasKey(u => u.Id);

        builder.Property(x => x.FullName)
                   .IsRequired()
                   .HasMaxLength(150);

        builder.Property(x => x.IsActive)
               .HasDefaultValue(true);

        builder.HasIndex(u => u.NormalizedUserName).IsUnique();
        builder.HasIndex(u => u.NormalizedEmail).IsUnique();

        builder.Property(u => u.Email).HasMaxLength(180);
        builder.Property(u => u.NormalizedEmail).HasMaxLength(180);
        builder.Property(u => u.UserName).HasMaxLength(180);
        builder.Property(u => u.NormalizedUserName).HasMaxLength(180);
        builder.Property(u => u.PhoneNumber).HasMaxLength(20);
        builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

        builder.HasMany<IdentityUserClaim<Guid>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
        builder.HasMany<IdentityUserLogin<Guid>>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
        builder.HasMany<IdentityUserToken<Guid>>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
        builder.HasMany<IdentityUserRole<Guid>>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
    }
}
