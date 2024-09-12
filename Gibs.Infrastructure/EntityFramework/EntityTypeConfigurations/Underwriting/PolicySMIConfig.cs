using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class PolicySMIConfiguration : IEntityTypeConfiguration<PolicySMI>
    {
        public void Configure(EntityTypeBuilder<PolicySMI> builder)
        {
            builder.ToTable("SMIs", "policy")
                   .HasKey(x => new { x.DeclareNo, x.SectionId, x.SmiId/*, x.SN*/ });

            builder.HasOne<ProductSMI>()
                   .WithMany()
                   .HasForeignKey(x => new { x.ProductId, x.SectionId, x.SmiId });

            builder.Property(x => x.SerialNo).HasColumnName("SN").ValueGeneratedOnAdd();
            builder.Property(x => x.DeclareNo).HasColumnName("DeclareNo");
            builder.Property(x => x.ProductId).HasColumnName("ProductID");
            builder.Property(x => x.SectionId).HasColumnName("SectionID");
            builder.Property(x => x.SmiId).HasColumnName("SMIID");

            builder.Property(x => x.SmiName).HasColumnName("SMIName");
            builder.Property(x => x.Description).HasColumnName("Description");
            //builder.Property(x => x.CertificateNo).HasColumnName("CertificateNo");

            builder.Property(x => x.PremiumRate).HasColumnName("PremiumRate");
            builder.Property(x => x.TotalSumInsured).HasColumnName("TotalSumInsured");
            builder.Property(x => x.TotalPremium).HasColumnName("TotalPremium");
            builder.Property(x => x.ShareSumInsured).HasColumnName("ShareSumInsured");
            builder.Property(x => x.SharePremium).HasColumnName("SharePremium");

            builder.Property<string>("_policyNo").HasColumnName("PolicyNo");

            //builder.Property(x => x.PolicyNo).HasColumnName("PolicyNo");
            //builder.Property(x => x.SectionName).HasColumnName("Location");
            //builder.Property(x => x.SmiName).HasColumnName("SMIDDetail");
            ////builder.Property(x => x.SectionID).HasColumnName("Field1"); // will it work?
            //builder.Property<string>("_tag").HasColumnName("Tag").HasValueOnAdd("NEW");
            //builder.Property<string>("_transGuid").HasColumnName("TransGUID").HasValueOnAdd("0");

            builder.Property(x => x.CreatedBy).HasColumnName("CreatedBy");
            builder.Property(x => x.CreatedUtc).HasColumnName("CreatedUtc");
            builder.Property(x => x.LastModifiedBy).HasColumnName("LastModifiedBy");
            builder.Property(x => x.LastModifiedUtc).HasColumnName("LastModifiedUtc");
        }
    }
}