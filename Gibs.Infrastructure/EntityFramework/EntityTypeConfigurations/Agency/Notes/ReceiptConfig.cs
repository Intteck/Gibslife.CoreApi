//using Gibs.Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Gibs.Infrastructure.EntityFramework.Configuration
//{
//    class ReceiptConfig : IEntityTypeConfiguration<Receipt>
//    {
//        public void Configure(EntityTypeBuilder<Receipt> builder)
//        {
//            builder.ToTable("Receipts", "agency")
//                   .HasQueryFilter(b => EF.Property<string>(b, "_noteType") == "RCP")
//                   .HasKey(x => x.ReceiptNo);

//            //wasiu says this column should NOT be NULL --> DNCNNo
//            builder.Property<string>("_noteType").HasColumnName("NoteType").HasValueOnAdd("RCP");
//            builder.Property<string>("_debitNoteNo").HasColumnName("DNCNNo").HasValueOnAdd("n/a");

//            builder.Property(x => x.AccountProcessing).HasColumnName("PropRate")
//                .HasConversion(x => (int)x, x => (ProcessCommissionEnum)x);

//            builder.Property(x => x.PolicyNo).HasColumnName("PolicyNo");
//            builder.Property(x => x.ReceiptNo).HasColumnName("ReceiptNo");
//            builder.Property(x => x.RefNoteNo).HasColumnName("refDNCNNo");  
//            builder.Property(x => x.DeclarationNo).HasColumnName("Field8");
//            builder.Property(x => x.GrossOrNetOption).HasColumnName("BizOption");
//            builder.Property(x => x.PrintNameOption).HasColumnName("BizSource");
//            builder.Property(x => x.CustomPrintName).HasColumnName("CoPolicyNo");
//            builder.Property(x => x.DepositorName).HasColumnName("Field1");
//            builder.Property(x => x.SecurityCode).HasColumnName("Field3");
//            builder.Property(x => x.Narration).HasColumnName("Narration");
//            builder.Property(x => x.HasTreaty).HasColumnName("HasTreaty");
//            //builder.Ignore(x => x.BatchGuid);
//            //builder.Property(x => x.BatchGuid).HasColumnName("TransGuid").HasConversion<string>();

//            //var payment = builder.OwnsOne(x => x.Payment);
//            //{
//            //    payment.Property(x => x.LedgerAccount).HasColumnName("Remarks"); 
//            //    payment.Property(x => x.BankAccountNo).HasColumnName("Field6"); 
//            //    payment.Property(x => x.BankAccountName).HasColumnName("Field7"); 
//            //    payment.Property(x => x.PaymentType).HasColumnName("PaymentType");
//            //    payment.Property(x => x.PaymentReference).HasColumnName("ChequeNo");
//            //    payment.Property(x => x.PaymentDate).HasColumnName("ExtraDate1");
//            //}

//            var member = builder.OwnsOne(x => x.Members);
//            {
//                member.Property(x => x.BranchID).HasColumnName("BranchID");
//                member.Property(x => x.BranchName).HasColumnName("Field4");
//                member.Property(x => x.ProductID).HasColumnName("SubRiskID");
//                member.Property(x => x.ProductName).HasColumnName("SubRisk");
//                member.Property(x => x.PartyID).HasColumnName("PartyID");
//                member.Property(x => x.PartyName).HasColumnName("Party");
//                member.Property(x => x.MarketerID).HasColumnName("MktStaffID");
//                member.Property(x => x.MarketerName).HasColumnName("MktStaff");
//                member.Ignore(x => x.SubChannelID);
//                member.Ignore(x => x.SubChannelName);
//                member.Property(x => x.CustomerID).HasColumnName("InsuredID");
//                member.Property(x => x.CustomerName).HasColumnName("InsuredName");
//                member.Property(x => x.LeadPartyID).HasColumnName("LeaderID");
//                member.Property(x => x.LeadPartyName).HasColumnName("Leader");
//                member.Property(x => x.ChannelID).HasColumnName("MktUnitID");
//                member.Property(x => x.ChannelName).HasColumnName("mktUnit");
//            }

//            var business = builder.OwnsOne(x => x.Business);
//            {
//                business.Ignore(x => x.SourceType);
//                business.Ignore(x => x.BusinessType);
//                business.Ignore(x => x.OurShareRate);
//                business.Ignore(x => x.FlatPremiumRate);
//                business.Ignore(x => x.StandardCoverDays);
//                business.Ignore(x => x.CommissionVatRate);
//                business.Ignore(x => x.CoverDays);

//                builder.Property<decimal>("_sumInsured").HasColumnName("SumInsured").HasValueOnAdd(0);
//                builder.Property<decimal>("_sumInsuredFx").HasColumnName("SumInsuredFrgn").HasValueOnAdd(0);
//                builder.Property<decimal>("_commission").HasColumnName("Commission").HasValueOnAdd(0);
//                builder.Property<decimal>("_vatAmount").HasColumnName("VatAmount").HasValueOnAdd(0);
//                builder.Property<decimal>("_vatRate").HasColumnName("VatRate").HasValueOnAdd(0);
//                builder.Property<decimal>("_prorataDays").HasColumnName("ProRataDays").HasValueOnAdd(0);

//                business.Property(x => x.CommissionRate).HasColumnName("PartyRate");
//                business.Property(x => x.CollectivePolicyNo).HasColumnName("CoPolicyNo");
//                business.Property(x => x.TransDate).HasColumnName("BillingDate");
//                business.Property(x => x.StartDate).HasColumnName("StartDate");
//                business.Property(x => x.EndDate).HasColumnName("EndDate");
//                business.Property(x => x.FxRate).HasColumnName("ExRate").HasConversion<double>();
//                business.Property(x => x.FxCurrencyID).HasColumnName("ExCurrency");
//            }

//            var secure = builder.OwnsOne(x => x.Secure);
//            {
//                secure.Property(x => x.Active).HasColumnName("Active");
//                secure.Property(x => x.Deleted).HasColumnName("Deleted");
//                secure.Property(x => x.CreatedBy).HasColumnName("SubmittedBy");
//                secure.Property(x => x.CreatedUtc).HasColumnName("SubmittedOn");
//                secure.Property(x => x.LastModifiedBy).HasColumnName("ModifiedBy");
//                secure.Property(x => x.LastModifiedUtc).HasColumnName("ModifiedOn");
//                secure.Property(x => x.ApprovedBy).HasColumnName("ApprovedBy");
//                secure.Property(x => x.ApprovedAt).HasColumnName("ApprovedOn");
//                secure.Property<string>("_appID").HasColumnName("CompanyID");
//                //this overrides the Global StringToEnum converter
//                secure.Property(x => x.ApprovalStatus).HasColumnName("Approval")
//                    .HasConversion(x => (byte?)x, x => (ApprovalStatusEnum?)x); 
//            }

//            var extended_fields = builder.OwnsOne(x => x.ExFields);
//            {
//                builder.Property(x => x.AmountPaid).HasColumnName("GrossPremium");
//                builder.Property(x => x.AmountPaidFx).HasColumnName("GrossPremiumFrgn");
//                builder.Property(x => x.BatchAmountPaid).HasColumnName("ProRataPremium");

//                extended_fields.Property(x => x.AmountPaidFx).HasColumnName("TotalRiskValue");
//                extended_fields.Property(x => x.AmountPaid).HasColumnName("NetAmount");
//            }

//            //var premium = builder.OwnsOne(x => x.Premium);
//            //{
//            //    premium.Ignore(x => x.BaseInsured);
//            //    premium.Ignore(x => x.BasePremium);

//            //    premium.Ignore(x => x.BaseInsuredFx);
//            //    premium.Ignore(x => x.BasePremiumFx);

//            //    premium.Property(x => x.Commission).HasColumnName("Commission");
//            //    premium.Property(x => x.CommissionVat).HasColumnName("VatAmount");

//            //    premium.Property(x => x.SumInsured).HasColumnName("SumInsured");
//            //    premium.Property(x => x.GrossPremium).HasColumnName("GrossPremium");

//            //    premium.Property(x => x.SumInsuredFx).HasColumnName("SumInsuredFrgn");
//            //    premium.Property(x => x.GrossPremiumFx).HasColumnName("GrossPremiumFrgn");

//            //    premium.Property(x => x.OurShareSumInsured).HasColumnName("A9");
//            //    premium.Property(x => x.OurSharePremium).HasColumnName("A10");

//            //    premium.Property(x => x.TotalRiskSumInsured).HasColumnName("TotalRiskValue");
//            //    premium.Property(x => x.TotalRiskGrossPremium).HasColumnName("TotalPremium");

//            //    premium.Property(x => x.ProratedPremium).HasColumnName("ProRataPremium");
//            //    premium.Property(x => x.NetProratedPremium).HasColumnName("NetAmount");
//            //}
//        }
//    }
//}