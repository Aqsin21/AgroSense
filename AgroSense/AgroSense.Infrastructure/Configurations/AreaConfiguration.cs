using AgroSense.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AgroSense.Infrastructure.Configurations
{
    public class AreaConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.OwnerUserId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(x => x.Latitude)
                .HasColumnType("decimal(9,6)");

            builder.Property(x => x.Longitude)
                .HasColumnType("decimal(9,6)");

            builder.HasIndex(x => x.OwnerUserId);
        }
    }
}
