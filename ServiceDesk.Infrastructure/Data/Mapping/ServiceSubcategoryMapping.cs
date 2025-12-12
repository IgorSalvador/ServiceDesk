using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Infrastructure.Data.Mapping;

public class ServiceSubcategoryMapping : IEntityTypeConfiguration<ServiceSubcategory>
{
    public void Configure(EntityTypeBuilder<ServiceSubcategory> builder)
    {
        builder.ToTable("ServiceSubcategories");

        builder.HasKey(ssc => ssc.Id);

        builder.Property(ssc => ssc.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasOne(ssc => ssc.ServiceCategory)
            .WithMany(sc => sc.Subcategories)
            .HasForeignKey(ssc => ssc.ServiceCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ssc => ssc.DefaultWorkflow)
            .WithMany()
            .HasForeignKey(ssc => ssc.DefaultWorkflowId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
