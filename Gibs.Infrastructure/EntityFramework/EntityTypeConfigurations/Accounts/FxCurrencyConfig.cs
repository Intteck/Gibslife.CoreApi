using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class FxCurrencyConfiguration : IEntityTypeConfiguration<FxCurrency>
    {
        public void Configure(EntityTypeBuilder<FxCurrency> builder)
        {
            builder.ToTable("FxCurrencies", "account")
                   .HasKey(x => x.Id);

            builder.HasMany(x => x.Rates)
                   .WithOne(x => x.Currency)
                   .HasForeignKey(x => x.CurrencyId);

            builder.Property(x => x.Id).HasColumnName("FxCurrencyID");
            builder.Property(x => x.Name).HasColumnName("FxCurrencyName");
            builder.Property(x => x.Active).HasColumnName("Active");
            builder.Property(x => x.Symbol).HasColumnName("Symbol");
            builder.Property(x => x.CurrentRate).HasColumnName("LastRate");
        }
    }
}