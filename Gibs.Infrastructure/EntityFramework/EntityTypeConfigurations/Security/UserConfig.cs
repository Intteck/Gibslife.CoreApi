using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "security")
                   .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("UserID");
            builder.Property(x => x.PasswordHash).HasColumnName("Password");
            builder.Property(x => x.FirstName).HasColumnName("FirstName");
            builder.Property(x => x.LastName).HasColumnName("LastName");

            builder.Property(x => x.Email).HasColumnName("Email");
            builder.Property(x => x.Phone).HasColumnName("Phone");
            builder.Property(x => x.Address).HasColumnName("Address");
            builder.Property(x => x.StaffNo).HasColumnName("StaffNo");
            builder.Property(x => x.AvatarUrl).HasColumnName("AvatarUrl");

            builder.Property(x => x.PasswordExpiryUtc).HasColumnName("PwdExpiryDate");
            builder.Property(x => x.LastLoginUtc).HasColumnName("LastLoginDate");
            builder.Property(x => x.IsActive).HasColumnName("Active");
            builder.Property(x => x.ApiKey).HasColumnName("ApiKey");
            builder.Property(x => x.Remarks).HasColumnName("Remarks");
        }
    }
}