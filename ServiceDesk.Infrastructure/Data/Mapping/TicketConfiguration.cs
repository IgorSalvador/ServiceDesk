using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Infrastructure.Data.Mapping;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Protocol)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.Priority)
            .IsRequired()
            .HasConversion<int>();

        builder.HasOne(x => x.CurrentQueue)
            .WithMany()
            .HasForeignKey(x => x.CurrentQueueId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CurrentStep)
            .WithMany()
            .HasForeignKey(x => x.CurrentStepId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Requester)
           .WithMany(u => u.RequestedTickets)
           .HasForeignKey(x => x.RequesterId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.AssignedUser)
           .WithMany(u => u.AssignedTickets)
           .HasForeignKey(x => x.AssignedUserId)
           .OnDelete(DeleteBehavior.Restrict);

        // ÍNDICES DE PERFORMANCE
        // 1. Dashboard principal: Tickets por Fila e Status
        builder.HasIndex(x => new { x.CurrentQueueId, x.Status });

        // 2. Busca por Protocolo
        builder.HasIndex(x => x.Protocol).IsUnique();

        // 3. Monitor de SLA (Job vai buscar onde Deadline < Agora e Status != Resolved)
        builder.HasIndex(x => new { x.CurrentStepDeadline, x.Status });
    }
}
