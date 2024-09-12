using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.ToTable("Risks", "product")
                   .HasKey(x => x.Id);

            //builder.HasMany<MidClass>().WithOne()
            //       .HasForeignKey(x => x.ClassID);

            //builder.HasMany<Product>().WithOne()
            //       .HasForeignKey(x => x.ClassID); 

            builder.Property(x => x.Id).HasColumnName("RiskID");
            builder.Property(x => x.Name).HasColumnName("RiskName");
         }
    }
}