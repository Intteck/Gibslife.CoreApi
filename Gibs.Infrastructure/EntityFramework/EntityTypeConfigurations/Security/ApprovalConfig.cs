using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class ApprovalsConfiguration : IEntityTypeConfiguration<Approval>
    {
        public void Configure(EntityTypeBuilder<Approval> builder)
        {
            builder.ToTable("Approvals", "security")
                   .HasKey(x => new { x.UserId, x.ApprovalId });

            builder.HasOne(x => x.User).WithMany(x => x.Approvals);

            builder.Property(x => x.UserId).HasColumnName("UserID");
            builder.Property(x => x.ApprovalId).HasColumnName("ApprovalID");
            builder.Property(x => x.MinValue).HasColumnName("MinValue");
            builder.Property(x => x.MaxValue).HasColumnName("MaxValue");

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