using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class BranchConfig : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branches", "common")
                   .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("BranchID");
            builder.Property(x => x.RegionId).HasColumnName("RegionID");
            builder.Property(x => x.StateId).HasColumnName("StateID");
            builder.Property(x => x.Name).HasColumnName("BranchName");
            builder.Property(x => x.AltName).HasColumnName("AltName");
            builder.Property(x => x.Address).HasColumnName("Address");
            builder.Property(x => x.Phone).HasColumnName("Phone");
            builder.Property(x => x.Tag).HasColumnName("Tag");
        }
    }
}