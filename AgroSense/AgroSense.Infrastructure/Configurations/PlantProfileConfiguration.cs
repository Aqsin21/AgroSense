using AgroSense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroSense.Infrastructure.Configurations
{
    public class PlantProfileConfiguration : IEntityTypeConfiguration<PlantProfile>
    {
        public void Configure(EntityTypeBuilder<PlantProfile> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.MinSoilMoisture)
                .HasColumnType("decimal(5,2)");

            builder.Property(x => x.MaxSoilMoisture)
                .HasColumnType("decimal(5,2)");

            builder.Property(x => x.MinAirHumidity)
                .HasColumnType("decimal(5,2)");

            builder.Property(x => x.MaxAirHumidity)
                .HasColumnType("decimal(5,2)");

            builder.Property(x => x.MinTemperature)
                .HasColumnType("decimal(5,2)");

            builder.Property(x => x.MaxTemperature)
                .HasColumnType("decimal(5,2)");

            builder.Property(x => x.DailyWaterNeedLiters)
                .HasColumnType("decimal(8,2)");

            builder.HasOne(x => x.PlantCategory)
                .WithMany(x => x.PlantProfiles)
                .HasForeignKey(x => x.PlantCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.PlantCategoryId, x.Name })
                .IsUnique();
        }
    }
}