using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ServiceDesk.Infrastructure.Data.Mapping.Account;

public class ApplicationUserTokenMapping : IEntityTypeConfiguration<IdentityUserToken<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<Guid>> builder)
    {
        builder.ToTable("ApplicationUserToken");

        builder.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

        builder.Property(t => t.LoginProvider)
            .HasMaxLength(120);

        builder.Property(t => t.Name)
            .HasMaxLength(180);
    }
}
