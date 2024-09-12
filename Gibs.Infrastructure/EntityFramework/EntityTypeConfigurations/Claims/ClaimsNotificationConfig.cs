using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class ClaimsNotificationConfiguration : IEntityTypeConfiguration<ClaimNotify>
    {
        public void Configure(EntityTypeBuilder<ClaimNotify> builder)
        {
            builder.ToTable("Master", "claim")
                   .HasKey(x => x.NotifyNo);

            //builder.HasIndex(x => x.ClaimNo)
            //       .IsUnique();

            builder.Property(x => x.NotifyNo).HasColumnName("NotifyNo");
            builder.Property(x => x.ClaimNo).HasColumnName("ClaimNo");

            var member = builder.OwnsOne(x => x.Members);
            {
                member.Property(x => x.BranchId).HasColumnName("BranchID");
                member.Property(x => x.BranchName).HasColumnName("BranchName");
                member.Property(x => x.ProductId).HasColumnName("ProductID");
                member.Property(x => x.ProductName).HasColumnName("ProductName");
                member.Property(x => x.PartyId).HasColumnName("PartyID");
                member.Property(x => x.PartyName).HasColumnName("PartyName");
                member.Property(x => x.CustomerId).HasColumnName("CustomerID");
                member.Property(x => x.CustomerName).HasColumnName("CustomerName");
                //member.Property(x => x.LeadPartyId).HasColumnName("LeadPartyID");
                //member.Property(x => x.LeadPartyName).HasColumnName("LeadPartyName");

                //member.Ignore(x => x.MarketerID);
                //member.Ignore(x => x.MarketerName);
                member.Ignore(x => x.SubChannelId);
                member.Ignore(x => x.SubChannelName);
                member.Ignore(x => x.ChannelId);
                member.Ignore(x => x.ChannelName);
            }

            builder.Property(x => x.EntryDate).HasColumnName("EntryDate");
            builder.Property(x => x.NotifyDate).HasColumnName("NotifyDate");
            builder.Property(x => x.LossDate).HasColumnName("LossDate");
            builder.Property(x => x.LossDetails).HasColumnName("LossDetails");

            builder.Property(x => x.PolicyNo).HasColumnName("PolicyNo");
            builder.Property(x => x.RefDebitNoteNo).HasColumnName("refDNCNNo");
            builder.Property(x => x.UnderwritenYear).HasColumnName("UndYear");
            //builder.Property(x => x.DetailID).HasColumnName("DetailID");



            //var dnote = builder.OwnsOne(x => x.DebitNote);
            //{
            //    dnote.Property(x => x.BranchID).HasColumnName("BranchID");
            //    dnote.Property(x => x.BranchID).HasColumnName("BranchID");
            //    dnote.Property(x => x.BranchID).HasColumnName("BranchID");
            //    dnote.Property(x => x.BranchID).HasColumnName("BranchID");
            //    dnote.Property(x => x.BranchID).HasColumnName("BranchID");
            //    dnote.Property(x => x.BranchID).HasColumnName("BranchID");
            //    dnote.Property(x => x.BranchID).HasColumnName("BranchID");
            //    dnote.Property(x => x.BranchID).HasColumnName("BranchID");
            //    dnote.Property(x => x.BranchID).HasColumnName("BranchID");
            //    dnote.Property(x => x.BranchID).HasColumnName("BranchID");
            //}



            //builder.Property(x => x.Field1).HasColumnName("Field1"); //motor CoverType
            //builder.Property(x => x.Field2).HasColumnName("Field2"); //motor Reg No

            builder.Property(x => x.Status).HasColumnName("RegStatus");
            builder.Property(x => x.RefReceiptNo).HasColumnName("RefReceiptNo");
            builder.Property(x => x.SumInsured).HasColumnName("SumInsured");
            builder.Property(x => x.GrossPremium).HasColumnName("Premium");
            builder.Property(x => x.Outstanding).HasColumnName("Outstanding");


            var business = builder.OwnsOne(x => x.Business);
            {
                business.Ignore(x => x.SourceType);
                business.Ignore(x => x.TransDate);
                business.Ignore(x => x.CoverDays);
                business.Ignore(x => x.FxRate);
                business.Ignore(x => x.FxCurrencyId);
                business.Ignore(x => x.FlatPremiumRate);
                business.Ignore(x => x.BusinessType);
                business.Ignore(x => x.CommissionRate);
                business.Ignore(x => x.CommissionVatRate);
                business.Ignore(x => x.CollectivePolicyNo);
                business.Ignore(x => x.StandardCoverDays);
                business.Property(x => x.StartDate).HasColumnName("StartDate");
                business.Property(x => x.EndDate).HasColumnName("EndDate");
                business.Property(x => x.OurShareRate).HasColumnName("A1"); 
            }

            var secure = builder; //builder.OwnsOne(x => x.Secure);
            {
                secure.Property(x => x.CreatedBy).HasColumnName("SubmittedBy");
                secure.Property(x => x.CreatedUtc).HasColumnName("SubmittedOn");
                secure.Property(x => x.LastModifiedBy).HasColumnName("ModifiedBy");
                secure.Property(x => x.LastModifiedUtc).HasColumnName("ModifiedOn");
                secure.Property(x => x.ApprovedBy).HasColumnName("ApprovedBy");
                secure.Property(x => x.ApprovedUtc).HasColumnName("ApprovedUtc");
                secure.Property(x => x.ApprovalStatus).HasColumnName("Approval")
                    .HasConversion(x => (byte?)x, x => (ApprovalEnum?)x);
                //secure.Property(x => x.Active).HasColumnName("Active");
                //secure.Ignore(x => x.Deleted);
            }
        }
    }
}