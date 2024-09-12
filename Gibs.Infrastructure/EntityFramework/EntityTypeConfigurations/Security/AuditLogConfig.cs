using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class AuditLogConfig : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLog", "security")
                   .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("AuditLogID");
            builder.Property(x => x.ActionId).HasColumnName("ActionID");

            builder.Property(x => x.ModuleId).HasColumnName("ModuleID");
            builder.Property(x => x.Category).HasColumnName("Category");
            builder.Property(x => x.Description).HasColumnName("Description");

            builder.Property(x => x.UserId).HasColumnName("CreatedBy");
            builder.Property(x => x.IpAddress).HasColumnName("CreatedIP");
            builder.Property(x => x.EntryTimeUtc).HasColumnName("CreatedUtc");
        }
    }
}