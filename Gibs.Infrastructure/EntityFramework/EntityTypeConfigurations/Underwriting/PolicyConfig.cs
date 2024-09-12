using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class PolicyConfig : IEntityTypeConfiguration<Policy>
    {
        public void Configure(EntityTypeBuilder<Policy> builder)
        {
            builder.ToTable("Master", "policy")
                   .HasKey(x => x.PolicyNo);

            builder.HasMany(x => x.Histories)
                   .WithOne(x => x.Policy)
                   .HasForeignKey(x => x.PolicyNo);

            builder.HasMany(x => x.DebitNotes)
                   .WithOne() //(x => x.Policy)
                   .HasForeignKey(x => x.PolicyNo);

            //builder.Ignore(x => x.Product);
            builder.Ignore(x => x.Customer);

            builder.Property(x => x.SerialNo).HasColumnName("SN").ValueGeneratedOnAdd();
            builder.Property(x => x.PolicyNo).HasColumnName("PolicyNo");
            builder.Property(x => x.NaicomId).HasColumnName("NaicomUID");

            var insured = builder.OwnsOne(x => x.Insured);
            {
                insured.Property(x => x.FullName).HasColumnName("FullName");
                insured.Property(x => x.Address).HasColumnName("Address");
                insured.Property(x => x.Email).HasColumnName("Email");
                insured.Property(x => x.Phone).HasColumnName("Phone");
                insured.Property(x => x.PhoneAlt).HasColumnName("PhoneAlt");
            }

            var members = builder.OwnsOne(x => x.Members);
            {
                members.Property(x => x.PartyId).HasColumnName("PartyID");
                members.Property(x => x.PartyName).HasColumnName("PartyName");
                members.Property(x => x.BranchId).HasColumnName("BranchID");
                members.Property(x => x.BranchName).HasColumnName("BranchName");
                members.Property(x => x.ProductId).HasColumnName("ProductID");
                members.Property(x => x.ProductName).HasColumnName("ProductName");
                members.Property(x => x.CustomerId).HasColumnName("CustomerID");
                members.Property(x => x.CustomerName).HasColumnName("CustomerName");
                members.Property(x => x.MarketerId).HasColumnName("MarketerID");
                members.Property(x => x.MarketerName).HasColumnName("MarketerName");
                members.Property(x => x.ChannelId).HasColumnName("ChannelID");
                members.Property(x => x.ChannelName).HasColumnName("ChannelName");
                members.Property(x => x.SubChannelId).HasColumnName("SubChannelID");
                members.Property(x => x.SubChannelName).HasColumnName("SubChannelName");
                //members.Property(x => x.LeadPartyId).HasColumnName("LeadPartyID");
                //members.Property(x => x.LeadPartyName).HasColumnName("LeadPartyName");
            }

            var business = builder.OwnsOne(x => x.Business);
            {
                business.Property(x => x.CollectivePolicyNo).HasColumnName("CoPolicyNo");

                business.Property(x => x.TransDate).HasColumnName("TransDate");
                business.Property(x => x.StartDate).HasColumnName("StartDate");
                business.Property(x => x.EndDate).HasColumnName("EndDate");
                business.Property(x => x.CoverDays).HasColumnName("CoverDays");
                business.Property(x => x.StandardCoverDays).HasColumnName("StdCoverDays").HasDefaultValue(365);

                business.Property(x => x.SourceType).HasColumnName("SrcTypeID");
                business.Property(x => x.BusinessType).HasColumnName("BizTypeID");
                business.Property(x => x.AccountingType).HasColumnName("ActTypeID");

                business.Property(x => x.FxCurrencyId).HasColumnName("CurrencyID");
                business.Property(x => x.FxRate).HasColumnName("CurrencyRate");
                business.Property(x => x.OurShareRate).HasColumnName("OurShareRate");

                business.Ignore(x => x.FlatPremiumRate);
                business.Ignore(x => x.CommissionRate);
                business.Ignore(x => x.CommissionVatRate);
            }

            var premium = builder.OwnsOne(p => p.Premium);
            {
                premium.Ignore(x => x.BaseSumInsuredFx);
                premium.Ignore(x => x.BaseSumInsured);
                premium.Property(x => x.SumInsuredFx).HasColumnName("SumInsuredFx");
                premium.Property(x => x.SumInsured).HasColumnName("SumInsured");
                premium.Property(x => x.WholeSumInsured).HasColumnName("WholeSumInsured");
                premium.Property(x => x.ShareSumInsured).HasColumnName("ShareSumInsured");

                premium.Ignore(x => x.Commission);
                premium.Ignore(x => x.CommissionVat);

                premium.Ignore(x => x.BasePremiumFx);
                premium.Ignore(x => x.BasePremium);
                premium.Property(x => x.GrossPremiumFx).HasColumnName("GrossPremiumFx");
                premium.Property(x => x.GrossPremium).HasColumnName("GrossPremium");
                premium.Property(x => x.WholePremium).HasColumnName("WholePremium");
                premium.Property(x => x.SharePremium).HasColumnName("SharePremium");

                premium.Property(x => x.ProrataPremium).HasColumnName("ProrataPremium");
                premium.Property(x => x.NetProrataPremium).HasColumnName("NetPremium");
            }

            var audit = builder; // builder.OwnsOne(x => x.Audit);
            {
                audit.Property(x => x.CreatedBy).HasColumnName("CreatedBy");
                audit.Property(x => x.CreatedUtc).HasColumnName("CreatedUtc");
                audit.Property(x => x.LastModifiedBy).HasColumnName("LastModifiedBy");
                audit.Property(x => x.LastModifiedUtc).HasColumnName("LastModifiedUtc");
            }
        }
    }
}