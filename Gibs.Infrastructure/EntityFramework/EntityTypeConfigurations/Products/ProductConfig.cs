using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Master", "product")
                   .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ProductID");
            builder.Property(x => x.MidClassId).HasColumnName("MidRiskID");
            builder.Property(x => x.ClassId).HasColumnName("RiskID");

            builder.Property(x => x.Name).HasColumnName("ProductName");
            builder.Property(x => x.AltName).HasColumnName("AltName");
            //builder.Property(x => x.AutoNumNextClaimNo).HasColumnName("Deleted");   
            //builder.Property(x => x.AutoNumNextNotifyNo).HasColumnName("Active");
            builder.Property(x => x.NaicomCode).HasColumnName("NaicomCode");
        }
    }
}