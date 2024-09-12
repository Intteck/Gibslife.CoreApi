using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class BlobConfiguration : IEntityTypeConfiguration<Blob>
    {
        public void Configure(EntityTypeBuilder<Blob> builder)
        {
            builder.ToTable("Blobs", "common")
                   .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("BlobID");
            builder.Property(x => x.Data).HasColumnName("Data");
            builder.Property(x => x.CreatedAt).HasColumnName("CreatedUtc");
            builder.Property(x => x.ContentType).HasColumnName("ContentType");
            builder.Property(x => x.Description).HasColumnName("Description");
        }
    }
}