using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class ProductSectionConfiguration : IEntityTypeConfiguration<ProductSection>
    {
        public void Configure(EntityTypeBuilder<ProductSection> builder)
        {
            builder.ToTable("Sections", "product")
                .HasKey(x => new { x.ProductId, x.SectionId });

            builder.HasMany(x => x.SMIs)
                   .WithOne(x => x.Section)
                   .HasForeignKey(x => new { x.ProductId, x.SectionId });

            builder.Property(x => x.ProductId).HasColumnName("ProductID");
            builder.Property(x => x.SectionId).HasColumnName("SectionID");
            builder.Property(x => x.SectionName).HasColumnName("SectionName");
        }
    }
}    