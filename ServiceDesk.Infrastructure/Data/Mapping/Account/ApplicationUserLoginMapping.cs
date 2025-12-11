using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ServiceDesk.Infrastructure.Data.Mapping.Account;

public class ApplicationUserLoginMapping : IEntityTypeConfiguration<IdentityUserLogin<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<Guid>> builder)
    {
        builder.ToTable("ApplicationUserLogin");

        builder.HasKey(l => new { l.LoginProvider, l.ProviderKey });

        builder.Property(l => l.LoginProvider)
            .HasMaxLength(128);

        builder.Property(l => l.ProviderKey)
            .HasMaxLength(128);

        builder.Property(u => u.ProviderDisplayName)
            .HasMaxLength(255);
    }
}
