using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ServiceDesk.Infrastructure.Data.Mapping.Account;

public class ApplicationUserRoleMapping : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        builder.ToTable("ApplicationUserRole");

        builder.HasKey(r => new { r.UserId, r.RoleId });
    }
}
