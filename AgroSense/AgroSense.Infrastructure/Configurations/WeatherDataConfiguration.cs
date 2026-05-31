using AgroSense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AgroSense.Infrastructure.Configurations
{
    public class WeatherDataConfiguration : IEntityTypeConfiguration<WeatherData>
    {
        public void Configure(EntityTypeBuilder<WeatherData> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Temperature)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(x => x.Humidity)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(x => x.RainProbability)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(x => x.RainAmountMm)
                .HasColumnType("decimal(8,2)");

            builder.Property(x => x.WindSpeed)
                .HasColumnType("decimal(8,2)");

            builder.Property(x => x.ForecastDate)
                .IsRequired();

            builder.Property(x => x.CreatedDate)
                .IsRequired();

            builder.HasOne(x => x.Area)
                .WithMany(x => x.WeatherData)
                .HasForeignKey(x => x.AreaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.AreaId, x.ForecastDate });
        }
    }
}