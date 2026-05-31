using AgroSense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AgroSense.Infrastructure.Configurations
{
    public class PlantCategoryConfiguration : IEntityTypeConfiguration<PlantCategory>
    {
        public void Configure(EntityTypeBuilder<PlantCategory> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}
