using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class MidClassConfiguration : IEntityTypeConfiguration<MidClass>
    {
        public void Configure(EntityTypeBuilder<MidClass> builder)
        {
            builder.ToTable("MidRisks", "product")
                   .HasKey(x => x.Id);

            //builder.HasMany<Product>().WithOne()
            //       .HasForeignKey(x => x.ClassID);
            //builder.HasMany(x => x.Products)
            //       .WithOne();

            builder.Property(x => x.ClassId).HasColumnName("RiskID");
            builder.Property(x => x.Id).HasColumnName("MidRiskID");
            builder.Property(x => x.Name).HasColumnName("MidRiskName");
         }
    }
}