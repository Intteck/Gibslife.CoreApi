using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class ProductFieldConfiguration : IEntityTypeConfiguration<ProductField>
    {
        public void Configure(EntityTypeBuilder<ProductField> builder)
        {
            builder.ToTable("Fields", "product")
                   .HasKey(x => new {x.CodeType, x.CodeId, x.FieldId });

            builder.Property(x => x.CodeType).HasColumnName("CodeTypeID");
            builder.Property(x => x.CodeId).HasColumnName("CodeID");
            builder.Property(x => x.FieldId).HasColumnName("FieldID");
            builder.Property(x => x.FieldType).HasColumnName("DataTypeID");
            builder.Property(x => x.DbSectionField).HasColumnName("DbSectionField");
            builder.Property(x => x.DbHistoryField).HasColumnName("DbHistoryField");
            builder.Property(x => x.Description).HasColumnName("Description");
            builder.Property(x => x.IsRequired).HasColumnName("IsRequired").HasConversion<byte>();
            builder.Property(x => x.IsParent).HasColumnName("IsParent").HasConversion<byte>();

            builder.Property(x => x.DefaultValue).HasColumnName("DefValue");
            builder.Property(x => x.MinimumValue).HasColumnName("MinValue");
            builder.Property(x => x.MaximumValue).HasColumnName("MaxValue");

            builder.Property(x => x.GroupName).HasColumnName("Group");
            builder.Property(x => x.SerialNo).HasColumnName("Serial").HasConversion<short>();
        }
    }
}