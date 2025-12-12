using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Infrastructure.Data.Mapping;

public class QueueMemberMapping : IEntityTypeConfiguration<QueueMember>
{
    public void Configure(EntityTypeBuilder<QueueMember> builder)
    {
        builder.ToTable("QueueMembers");

        builder.HasKey(x => new { x.ServiceQueueId, x.UserId });

        // Index para performance: Buscar todas as filas de um usuário rapidamente
        builder.HasIndex(x => x.UserId);

        builder.HasOne(x => x.User)
           .WithMany(u => u.QueueMemberships)
           .HasForeignKey(x => x.UserId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}
