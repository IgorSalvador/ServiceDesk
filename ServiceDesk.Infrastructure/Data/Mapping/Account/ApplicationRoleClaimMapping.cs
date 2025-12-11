using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ServiceDesk.Infrastructure.Data.Mapping.Account;

public class ApplicationRoleClaimMapping : IEntityTypeConfiguration<IdentityRoleClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<Guid>> builder)
    {
        builder.ToTable("ApplicationRoleClaim");

        builder.HasKey(rc => rc.Id);

        builder.Property(u => u.ClaimType)
            .HasMaxLength(255);

        builder.Property(u => u.ClaimValue)

            .HasMaxLength(255);
    }
}
