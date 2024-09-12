using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class SalesChannelConfig : IEntityTypeConfiguration<SalesChannel>
    {
        public void Configure(EntityTypeBuilder<SalesChannel> builder)
        {
            builder.ToTable("Channels", "common")
                   .HasKey(x => x.Id);

            builder.HasMany<SalesSubChannel>().WithOne()
                   .HasForeignKey(x => x.ChannelId);

            builder.HasOne<Branch>().WithMany()
                   .HasForeignKey(x => x.BranchId);

            //builder.HasMany(x => x.SubChannels)
            //       .WithOne(x => x.Channel)
            //       .HasForeignKey(x => x.ChannelID)
            //       .HasPrincipalKey(x => x.ChannelID);

            builder.Property(x => x.BranchId).HasColumnName("BranchID");
            builder.Property(x => x.Id).HasColumnName("ChannelID");
            builder.Property(x => x.Name).HasColumnName("ChannelName");
            builder.Property(x => x.AltName).HasColumnName("AltName");
        }
    }
}