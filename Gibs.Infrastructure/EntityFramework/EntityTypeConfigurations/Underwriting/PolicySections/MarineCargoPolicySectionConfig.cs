using GibsPro.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.CIP_DataAccess.DataContexts.Configuration
{
    class MarineCargoPolicySectionConfig : IEntityTypeConfiguration<MarineCargoPolicySection>
    {
        public void Configure(EntityTypeBuilder<MarineCargoPolicySection> builder)
        {
            var v = "varchar(1240)";
            var rd = builder.OwnsOne(p => p.RiskDetail);

            rd.Property(x => x._ShipMode).HasColumnName("Field1").HasColumnType(v);
            rd.Property(x => x._CertificateType).HasColumnName("Field2").HasColumnType(v);
            rd.Property(x => x.CertificateNo).HasColumnName("ENDTNum");//.HasColumnType(v);
            rd.Ignore(x => x.InvoiceDate);
            rd.Property(x => x.SubjectMatter).HasColumnName("SectionID");//.HasColumnType(v);
            rd.Property(x => x.VoyageFrom).HasColumnName("Location");//.HasColumnType(v);

            rd.Property(x => x.VoyageTo).HasColumnName("Field3").HasColumnType(v);
            rd.Property(x => x._EndoresementNo).HasColumnName("Field4").HasColumnType(v);
            rd.Property(x => x.MarksAndNumbers).HasColumnName("Field5").HasColumnType(v); 
            rd.Property(x => x.Conveyance).HasColumnName("Field6").HasColumnType(v);
            rd.Property(x => x.CoverType).HasColumnName("Field7").HasColumnType(v);
            rd.Property(x => x.ExcessClause).HasColumnName("Field8").HasColumnType(v);
            rd.Property(x => x.LienClause).HasColumnName("Field9").HasColumnType(v);
            rd.Property(x => x.InvoiceNo).HasColumnName("Field10").HasColumnType(v); 
            rd.Property(x => x.TaxIdNumber).HasColumnName("Field11").HasColumnType(v);   
            rd.Property(x => x._BasisOfValuation).HasColumnName("Field13").HasColumnType(v);
            rd.Property(x => x._CurrencyID).HasColumnName("Field14").HasColumnType(v);
            rd.Property(x => x.NatureOfCargo).HasColumnName("Field15").HasColumnType(v);
            rd.Property(x => x._VesselDescription).HasColumnName("ContentSection").HasColumnType(v);

            rd.Property(x => x._A1).HasColumnName("A1");
            rd.Property(x => x._OurShareRate).HasColumnName("A2");
            rd.Property(x => x._FxRate).HasColumnName("A3");
            rd.Property(x => x._ValuationRate).HasColumnName("A4");
            rd.Property(x => x._A5).HasColumnName("A5");
            rd.Property(x => x._ExtendCoverPrem).HasColumnName("A6");
            rd.Property(x => x._ExtendCoverSum).HasColumnName("A7");
            rd.Property(x => x._InvoiceRiskValue).HasColumnName("A8");
            rd.Property(x => x._FxRatePercentage).HasColumnName("A9");
            rd.Property(x => x._TotalRiskItemSum).HasColumnName("A10");
            rd.Property(x => x._TotalRiskItemPrem).HasColumnName("A11");
            rd.Property(x => x._PremiumRate).HasColumnName("A17");
            rd.Property(x => x._RiskProRataPrem).HasColumnName("A36");
            rd.Property(x => x._NetB4ProrataPrem).HasColumnName("A37");
        }
    }
}