using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", "agency")
                   .HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("CustomerID");
            builder.Property(x => x.FullName).HasColumnName("CustomerName"); 
            builder.Property(x => x.Type).HasColumnName("CustomerTypeID");
            builder.Property(x => x.BirthDate).HasColumnName("BirthDate");

            builder.Property(x => x.Email).HasColumnName("Email");
            builder.Property(x => x.Phone).HasColumnName("Phone");
            builder.Property(x => x.PhoneAlt).HasColumnName("PhoneAlt");

            builder.Property(x => x.Address).HasColumnName("Address");
            builder.Property(x => x.CityLGA).HasColumnName("CityLGA");
            builder.Property(x => x.StateId).HasColumnName("StateID");
            builder.Property(x => x.Country).HasColumnName("Country");

            builder.Property(x => x.TaxNumber).HasColumnName("TaxNumber");
            builder.Property(x => x.KycNumber).HasColumnName("KycNumber");
            builder.Property(x => x.KycTypeId).HasColumnName("KycTypeID");
            builder.Property(x => x.KycIssueDate).HasColumnName("KycIssueDate");
            builder.Property(x => x.KycExpiryDate).HasColumnName("KycExpiryDate");

            builder.Property(x => x.Title).HasColumnName("Title");  
            builder.Property(x => x.LastName).HasColumnName("LastName");
            builder.Property(x => x.FirstName).HasColumnName("FirstName");
            builder.Property(x => x.OtherNames).HasColumnName("OtherNames");
            //builder.Property(x => x.Gender).HasColumnName("GenderID");

            builder.Property(x => x.Industry).HasColumnName("Industry");
            builder.Property(x => x.RiskProfile).HasColumnName("RiskProfileID");
            builder.Property(x => x.NextOfKin).HasColumnName("ContactPerson");
        }
    }
}