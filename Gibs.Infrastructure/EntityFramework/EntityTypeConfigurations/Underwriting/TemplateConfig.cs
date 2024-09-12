using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class TemplateDocConfig : IEntityTypeConfiguration<TemplateDoc>
    {
        public void Configure(EntityTypeBuilder<TemplateDoc> builder)
        {
            builder.ToTable("Templates", "policy")
                   .HasKey(x => x.Id);

            builder.HasOne(x => x.Blob).WithOne()
                   .OnDelete(DeleteBehavior.ClientCascade);

            builder.Property(x => x.Id).HasColumnName("TemplateID");
            builder.Property(x => x.TypeId).HasColumnName("TypeID");
            builder.Property(x => x.BlobId).HasColumnName("BlobID");
            builder.Property(x => x.Description).HasColumnName("Description");

            var audit = builder;
            {
                audit.Property(x => x.CreatedBy).HasColumnName("CreatedBy");
                audit.Property(x => x.CreatedUtc).HasColumnName("CreatedUtc");
                audit.Property(x => x.LastModifiedBy).HasColumnName("LastModifiedBy");
                audit.Property(x => x.LastModifiedUtc).HasColumnName("LastModifiedUtc");
            }
        }
    }
}