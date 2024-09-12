using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class FxRateConfiguration : IEntityTypeConfiguration<FxRate>
    {
        public void Configure(EntityTypeBuilder<FxRate> builder)
        {
            builder.ToTable("FxRates", "account")
                   .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("FxRateID");
            builder.Property(x => x.CurrencyId).HasColumnName("CurrencyID");
            builder.Property(x => x.RateValue).HasColumnName("Value");

            builder.Property(x => x.CreatedBy).HasColumnName("CreatedBy");
            builder.Property(x => x.CreatedUtc).HasColumnName("CreatedUtc");
            builder.Ignore(x => x.LastModifiedUtc);
            builder.Ignore(x => x.LastModifiedBy);
        }
    }
}