using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class MarketerConfig : IEntityTypeConfiguration<Marketer>
    {
        public void Configure(EntityTypeBuilder<Marketer> builder)
        {
            builder.ToTable("Marketers", "agency")
                   .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("MarketerID");
            builder.Property(x => x.ChannelId).HasColumnName("ChannelID");
            builder.Property(x => x.SubChannelId).HasColumnName("SubChannelID");
            builder.Property(x => x.FullName).HasColumnName("FullName");
            builder.Property(x => x.Active).HasColumnName("Active");

            //builder.Property(x => x.GroupName).HasColumnName("GroupName");

            //builder.Property(x => x.YearStart).HasColumnName("Budyear1");
            //builder.Property(x => x.YearEnd).HasColumnName("Budyear2");
            //builder.Property(x => x.CurrentTarget).HasColumnName("CurTarget");
            //builder.Property(x => x.PreviousTarget).HasColumnName("PrevTarget");

            var secure = builder; // builder.OwnsOne(x => x.Secure);
            {
                secure.Property(x => x.CreatedBy).HasColumnName("CreatedBy");
                secure.Property(x => x.CreatedUtc).HasColumnName("CreatedUtc");
                secure.Property(x => x.LastModifiedBy).HasColumnName("LastModifiedBy");
                secure.Property(x => x.LastModifiedUtc).HasColumnName("LastModifiedUtc");
            }
        }
    }
}