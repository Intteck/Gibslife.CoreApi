using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class CommRateConfig : IEntityTypeConfiguration<CommRate>
    {
        public void Configure(EntityTypeBuilder<CommRate> builder)
        {
            builder.ToTable("Rates", "agency")
                   .HasKey(x => new { x.RiskOptionId, x.PartyOptionId });

            builder.Property(x => x.RiskOptionId).HasColumnName("RiskOptionID");
            builder.Property(x => x.PartyOptionId).HasColumnName("PartyOptionID");
            builder.Property(x => x.ComRate).HasColumnName("CommRate");
            builder.Property(x => x.VatRate).HasColumnName("VatRate");
            builder.Property(x => x.Remarks).HasColumnName("Remarks");
        }
    }
}