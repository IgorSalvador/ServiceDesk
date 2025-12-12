using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Infrastructure.Data.Mapping;

public class ServiceQueueMapping : IEntityTypeConfiguration<ServiceQueue>
{
    public void Configure(EntityTypeBuilder<ServiceQueue> builder)
    {
        builder.ToTable("ServiceQueues");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(250);

        builder.HasMany(x => x.Members)
            .WithOne(x => x.ServiceQueue)
            .HasForeignKey(x => x.ServiceQueueId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
