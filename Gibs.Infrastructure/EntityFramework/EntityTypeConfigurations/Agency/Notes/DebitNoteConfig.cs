using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class DebitNoteConfig : IEntityTypeConfiguration<DebitNote>
    {
        public void Configure(EntityTypeBuilder<DebitNote> builder)
        {
            builder.ToTable("Notes", "agency")
                   .HasQueryFilter(b => EF.Property<string>(b, "_noteTypeID") == "DN")
                   .HasKey(x => x.DebitNoteNo);

            builder.Property<string>("_noteTypeID").HasColumnName("NoteTypeID").HasValueOnAdd("DN");
            builder.Property(x => x.PolicyNo).HasColumnName("PolicyNo");
            builder.Property(x => x.DebitNoteNo).HasColumnName("NoteNo");
            builder.Property(x => x.DeclarationNo).HasColumnName("DeclarationNo");

            builder.Property(x => x.BranchId).HasColumnName("BranchID");
            builder.Property(x => x.Narration).HasColumnName("Narration");

            builder.Property(x => x.PartyId).HasColumnName("PartyID");
            builder.Property(x => x.PartyName).HasColumnName("PartyName");
            builder.Property(x => x.CurrencyId).HasColumnName("FxCurrencyID");
            builder.Property(x => x.FxRate).HasColumnName("FxRate");

            //builder.Property(x => x.DebitNoteType).HasColumnName("DebitNoteTypeID");
            //builder.Property(x => x.Endorsement).HasColumnName("EndorsementID");
            //builder.Property(x => x.HasTreaty).HasColumnName("HasTreaty");
            //builder.Property(x => x.BatchGuid).HasColumnName("BatchGuid");


            //var premium = builder.OwnsOne(x => x.Premium);
            //{
            //    premium.Ignore(x => x.BaseInsured);
            //    premium.Ignore(x => x.BasePremium);

            //    premium.Ignore(x => x.BaseInsuredFx);
            //    premium.Ignore(x => x.BasePremiumFx);

            //    premium.Property(x => x.SumInsured).HasColumnName("SumInsured");
            //    premium.Property(x => x.GrossPremium).HasColumnName("Premium");

            //    premium.Property(x => x.SumInsuredFx).HasColumnName("SumInsuredFx");
            //    premium.Property(x => x.GrossPremiumFx).HasColumnName("PremiumFx");

            //    premium.Property(x => x.OurShareSumInsured).HasColumnName("ShareSumInsured");
            //    premium.Property(x => x.OurSharePremium).HasColumnName("SharePremium");

            //    premium.Property(x => x.TotalRiskSumInsured).HasColumnName("TotalRiskSumInsured");
            //    premium.Property(x => x.TotalRiskGrossPremium).HasColumnName("TotalRiskPremium");

            //    premium.Property(x => x.ProratedPremium).HasColumnName("ProrataPremium");
            //    premium.Property(x => x.NetProratedPremium).HasColumnName("NetProrataPremium");

            //    premium.Property(x => x.Commission).HasColumnName("Commission");
            //    premium.Property(x => x.CommissionVat).HasColumnName("CommissionVat");
            //}

            var secure = builder; // builder.OwnsOne(x => x.Secure);
            {
                secure.Ignore(x => x.LastModifiedBy);
                secure.Ignore(x => x.LastModifiedUtc);
                //secure.Property(x => x.CreatedBy).HasColumnName("CreatedBy");
                //secure.Property(x => x.CreatedUtc).HasColumnName("CreatedUtc");
                //secure.Property(x => x.ApprovedBy).HasColumnName("ApprovedBy");
                //secure.Property(x => x.ApprovedUtc).HasColumnName("ApprovedUtc");
                secure.Property(x => x.ApprovalStatus).HasColumnName("Approval").HasConversion<string>();
            }

            var naicom = builder.OwnsOne(x => x.Naicom);
            {
                //naicom.Property(x => x.UniqueID).HasColumnName("Z_NAICOM_UID");
                //naicom.Property(x => x.Status).HasColumnName("Z_NAICOM_STATUS").HasConversion<string>();
                //naicom.Property(x => x.SubmitDate).HasColumnName("Z_NAICOM_SENT_ON");
                //naicom.Property(x => x.ErrorMessage).HasColumnName("Z_NAICOM_ERROR");
                //naicom.Property(x => x.JsonPayload).HasColumnName("Z_NAICOM_JSON");
            }
        }
    }
}