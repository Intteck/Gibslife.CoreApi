using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class ProductSMIConfiguration : IEntityTypeConfiguration<ProductSMI>
    {
        public void Configure(EntityTypeBuilder<ProductSMI> builder)
        {
            builder.ToTable("SMIs", "product")
                   .HasKey(x => new { x.ProductId, x.SectionId, x.SmiId });

            builder.Property(x => x.ProductId).HasColumnName("ProductID");
            builder.Property(x => x.SectionId).HasColumnName("SectionID");
            builder.Property(x => x.SmiId).HasColumnName("SmiID");
            builder.Property(x => x.SmiName).HasColumnName("SmiName");
        }
    }
}