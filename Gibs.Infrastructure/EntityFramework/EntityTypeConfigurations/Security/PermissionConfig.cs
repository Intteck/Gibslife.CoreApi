using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class PermissionsConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions", "security")
                   .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("PermissionID");
            builder.Property(x => x.Name).HasColumnName("PermissionName");
            //builder.Property(x => x.Category).HasColumnName("Category");
            //builder.Property(x => x.Remarks).HasColumnName("Remarks");
            //builder.Property(x => x.Module).HasColumnName("Module");
         }
    }
}