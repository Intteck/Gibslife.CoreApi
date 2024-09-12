using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class PartyConfig : IEntityTypeConfiguration<Party>
    {
        public void Configure(EntityTypeBuilder<Party> builder)
        {
            builder.ToTable("Parties", "agency")
                   .HasKey(x => x.Id);

            builder.Ignore(x => x.Type);
            builder.Property(x => x.Id).HasColumnName("PartyID");
            builder.Property(x => x.FullName).HasColumnName("PartyName");
            builder.Property(x => x.TypeId).HasColumnName("PartyTypeID");
            builder.Property(x => x.BirthDate).HasColumnName("BirthDate");

            builder.Property(x => x.Email).HasColumnName("Email");
            builder.Property(x => x.Phone).HasColumnName("Phone");
            builder.Property(x => x.PhoneAlt).HasColumnName("PhoneAlt");

            builder.Property(x => x.Address).HasColumnName("Address");
            builder.Property(x => x.CityLGA).HasColumnName("CityLGA");
            builder.Property(x => x.StateId).HasColumnName("StateID");
            builder.Property(x => x.Country).HasColumnName("Country");

            builder.Property(x => x.NextOfKin).HasColumnName("ContactPerson");
            //builder.Property(x => x.CreditLimit).HasColumnName("CreditLimit");
            builder.Property(x => x.CommTypeId).HasColumnName("CommTypeID");

            //builder.Property(x => x.Gender).HasColumnName("Fax");
            //builder.Property(x => x.Password).HasColumnName("Tag");
            //builder.Property(x => x.Deleted).HasColumnName("Deleted");
            //builder.Property(x => x.Active).HasColumnName("Active").HasConversion<byte>();

            builder.Property(x => x.TaxNumber).HasColumnName("TaxNumber");
            builder.Property(x => x.KycNumber).HasColumnName("KycNumber");
            builder.Property(x => x.KycTypeId).HasColumnName("KycTypeID");
            builder.Property(x => x.KycIssueDate).HasColumnName("KycIssueDate");
            builder.Property(x => x.KycExpiryDate).HasColumnName("KycExpiryDate");
        }
    }
}