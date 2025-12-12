using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceDesk.Domain.Entities;

namespace ServiceDesk.Infrastructure.Data.Mapping;

public class ServiceCategoryMapping : IEntityTypeConfiguration<ServiceCategory>
{
    public void Configure(EntityTypeBuilder<ServiceCategory> builder)
    {
        builder.ToTable("ServiceCategories");

        builder.HasKey(sc => sc.Id);

        builder.Property(sc => sc.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasMany(sc => sc.Subcategories)
            .WithOne(ssc => ssc.ServiceCategory)
            .HasForeignKey(ssc => ssc.ServiceCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
