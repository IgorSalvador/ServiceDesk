using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ServiceDesk.Infrastructure.Data.Mapping.Account;

public class ApplicationUserClaimMapping : IEntityTypeConfiguration<IdentityUserClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<Guid>> builder)
    {
        builder.ToTable("ApplicationClaim");

        builder.HasKey(uc => uc.Id);

        builder.Property(u => u.ClaimType)
            .HasMaxLength(255);

        builder.Property(u => u.ClaimValue)
            .HasMaxLength(255);
    }
}

