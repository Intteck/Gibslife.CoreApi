using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.CIP_DataAccess.DataContexts.Configuration
{
    class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups", "security")
                   .HasKey(x => x.Id);

            builder.HasMany(x => x.Users)
                   .WithMany(x => x.Groups)
                   .UsingEntity<Dictionary<string, object>>(
                       "GroupUsers", 
                       e => e.HasOne<User>().WithMany().HasForeignKey("UserID"),
                       e => e.HasOne<Group>().WithMany().HasForeignKey("GroupID"),
                       j => j.HasKey("GroupID", "UserID"));

            builder.Property(x => x.Id).HasColumnName("GroupID");
            builder.Property(x => x.Name).HasColumnName("GroupName");
        }
    }
}