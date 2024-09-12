using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.CIP_DataAccess.DataContexts.Configuration
{
    class RolesConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles", "security")
                   .HasKey(x => x.Id);

            builder.HasMany(x => x.Permissions)
                   .WithMany(x => x.Roles)
                   .UsingEntity<Dictionary<string, object>>(
                       "RolePermissions", 
                       e => e.HasOne<Permission>().WithMany().HasForeignKey("PermissionID"),
                       e => e.HasOne<Role>().WithMany().HasForeignKey("RoleID"),
                       j => j.HasKey("RoleID", "PermissionID"));

            builder.Property(x => x.Id).HasColumnName("RoleID");
            builder.Property(x => x.Name).HasColumnName("RoleName");
        }
    }
}