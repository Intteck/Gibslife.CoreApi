using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class RegionConfig : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable("Regions", "common")
                   .HasKey(x => x.Id);

            builder.HasMany(x => x.Branches).WithOne()
                   .HasForeignKey(x => x.RegionId);

            builder.Property(x => x.Id).HasColumnName("RegionID");
            builder.Property(x => x.Name).HasColumnName("RegionName");
            builder.Property(x => x.AltName).HasColumnName("AltName");
        }
    }
}