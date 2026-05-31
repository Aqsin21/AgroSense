using AgroSense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroSense.Infrastructure.Configurations
{
    public class SensorReadingConfiguration : IEntityTypeConfiguration<SensorReading>
    {
        public void Configure(EntityTypeBuilder<SensorReading> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Value)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(x => x.ReadAt)
                .IsRequired();

            builder.HasOne(x => x.Sensor)
                .WithMany(x => x.SensorReadings)
                .HasForeignKey(x => x.SensorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.SensorId, x.ReadAt });
        }
    }
}