using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Infrastructure.Data.Mapping;

public class WorkflowStepConfiguration : IEntityTypeConfiguration<WorkflowStep>
{
    public void Configure(EntityTypeBuilder<WorkflowStep> builder)
    {
        builder.ToTable("WorkflowSteps");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.StepName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.OrderIndex)
            .IsRequired();

        builder.Property(x => x.SLAInHours)
            .IsRequired();

        builder.HasOne(x => x.TargetQueue)
            .WithMany()
            .HasForeignKey(x => x.TargetQueueId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
