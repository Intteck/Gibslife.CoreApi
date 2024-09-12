using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class SettingConfig : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.ToTable("Settings", "common")
                   .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("SettingID");
            builder.Property(x => x.Name).HasColumnName("SettingName");
            builder.Property(x => x.DataType).HasColumnName("DataTypeID");
            builder.Property(x => x.MinValue).HasColumnName("MinValue");
            builder.Property(x => x.MaxValue).HasColumnName("MaxValue");
            builder.Property(x => x.DefValue).HasColumnName("DefValue");
            builder.Property(x => x.Value).HasColumnName("Value");
         }
    }
}