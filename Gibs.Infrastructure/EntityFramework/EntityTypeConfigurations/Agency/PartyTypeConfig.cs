using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class PartyTypeConfiguration : IEntityTypeConfiguration<PartyType>
    {
        public void Configure(EntityTypeBuilder<PartyType> builder)
        {
            builder.ToTable("PartyTypes", "agency")
                   .HasKey(x => x.Id);

            builder.HasMany(x => x.Parties)
                   .WithOne(x => x.PartyType)
                   .HasForeignKey(x => x.TypeId);

            builder.Property(x => x.Id).HasColumnName("PartyTypeID");
            builder.Property(x => x.Name).HasColumnName("PartyTypeName");
            builder.Property(x => x.CategoryId).HasColumnName("CategoryID");
        }
    }
}