using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class SignatureConfiguration : IEntityTypeConfiguration<Signature>
    {
        public void Configure(EntityTypeBuilder<Signature> builder)
        {
            builder.ToTable("Signatures", "security")
                   .HasKey(x => x.Id);

            builder.HasOne(x => x.Blob).WithOne()
                   .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(x => x.User).WithMany(x => x.Signatures);

            builder.Property(x => x.Id).HasColumnName("SignatureID");
            builder.Property(x => x.UserId).HasColumnName("UserID");
            builder.Property(x => x.BlobId).HasColumnName("BlobID");
            builder.Property(x => x.Description).HasColumnName("Description");
            builder.Property(x => x.CreatedAt).HasColumnName("CreatedUtc");
        }
    }
}