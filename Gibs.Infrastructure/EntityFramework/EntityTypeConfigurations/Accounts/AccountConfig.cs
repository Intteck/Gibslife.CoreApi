using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts", "account")
                   .HasKey(x => x.Id);

            builder.HasOne<ControlAccount>().WithMany(x => x.Accounts)
                   .HasForeignKey(x => x.ControlId);

            builder.Property(x => x.ControlId).HasColumnName("ControlID");
            builder.Property(x => x.Id).HasColumnName("AccountID");
            builder.Property(x => x.Name).HasColumnName("AccountName");
            builder.Property(x => x.Remarks).HasColumnName("Remarks");
        }
    }
}