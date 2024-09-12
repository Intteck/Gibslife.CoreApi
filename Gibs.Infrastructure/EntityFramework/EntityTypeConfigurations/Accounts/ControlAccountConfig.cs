using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class ControlAccountConfiguration : IEntityTypeConfiguration<ControlAccount>
    {
        public void Configure(EntityTypeBuilder<ControlAccount> builder)
        {
            builder.ToTable("ControlAccounts", "account")
                   .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ControlID");
            builder.Property(x => x.Name).HasColumnName("ControlName");
            builder.Property(x => x.Category).HasColumnName("CategoryID");
            builder.Property(x => x.Remarks).HasColumnName("Remarks");
        }
    }
}