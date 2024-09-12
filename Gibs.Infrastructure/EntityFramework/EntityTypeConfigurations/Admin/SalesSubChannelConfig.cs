using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class SalesSubChannelConfig : IEntityTypeConfiguration<SalesSubChannel>
    {
        public void Configure(EntityTypeBuilder<SalesSubChannel> builder)
        {
            builder.ToTable("SubChannels", "common")
                   .HasKey(x => x.Id);

            builder.Property(x => x.ChannelId).HasColumnName("ChannelID");
            builder.Property(x => x.Id).HasColumnName("SubChannelID");
            builder.Property(x => x.Name).HasColumnName("SubChannelName");
            builder.Property(x => x.AltName).HasColumnName("AltName");
        }
    }
}