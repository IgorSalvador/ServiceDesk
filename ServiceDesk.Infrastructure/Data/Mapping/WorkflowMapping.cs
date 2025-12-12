using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Infrastructure.Data.Mapping;

public class WorkflowMapping : IEntityTypeConfiguration<Workflow>
{
    public void Configure(EntityTypeBuilder<Workflow> builder)
    {
        builder.ToTable("Workflows");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Version)
            .IsRequired();

        builder.HasMany(x => x.Steps)
            .WithOne()
            .HasForeignKey(x => x.WorkflowId)
            .OnDelete(DeleteBehavior.Cascade); 
    }
}
