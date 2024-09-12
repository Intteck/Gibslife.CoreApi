using GibsPro.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.CIP_DataAccess.DataContexts.Configuration
{
    class MotorPolicySectionConfig : IEntityTypeConfiguration<MotorPolicySection>
    {
        public void Configure(EntityTypeBuilder<MotorPolicySection> builder)
        {
            var v = "varchar(1240)";
            var rd = builder.OwnsOne(p => p.RiskDetail);

            rd.Property(x => x.VehicleRegNo).HasColumnName("Field1").HasColumnType(v);
            rd.Property(x => x.VehicleTypeID).HasColumnName("Field2").HasColumnType(v);
            rd.Property(x => x.StateOfIssue).HasColumnName("Field3").HasColumnType(v);
            rd.Property(x => x.VehicleColour).HasColumnName("Field4").HasColumnType(v);
            rd.Property(x => x.ManufactureYear).HasColumnName("Field5").HasColumnType(v);
            rd.Property(x => x.EngineCapacityHP).HasColumnName("Field6").HasColumnType(v);
            rd.Property(x => x.EngineNumber).HasColumnName("Field7").HasColumnType(v);
            rd.Property(x => x.ChasisNumber).HasColumnName("Field8").HasColumnType(v);
            rd.Property(x => x.NumberOfSeats).HasColumnName("Field9").HasColumnType(v);
            rd.Property(x => x.VehicleUser).HasColumnName("ContentSection").HasColumnType(v);

            //riskDetail.Property(x => x.fdgdggs).HasColumnName("Field11").HasColumnType(v);   //tracking
            //riskDetail.Property(x => x.dsfsdfsfd).HasColumnName("Field12").HasColumnType(v); //rescue
            rd.Property(x => x.VehicleUsage).HasColumnName("Field13").HasColumnType(v);
            rd.Property(x => x.CoverType).HasColumnName("Field17").HasColumnType(v);    //cover type
            //riskDetail.Property(x => x.asdasdasda).HasColumnName("Field18").HasColumnType(v); // wax code
            //riskDetail.Property(x => x.sadadsasda).HasColumnName("Field19").HasColumnType(v); // endorse option
            //          // field 20 is special
            rd.Property(x => x._ExcessBuyBack).HasColumnName("Field21").HasColumnType(v); //excess details
            //riskDetail.Property(x => x.Field22).HasColumnName("Field22").HasColumnType(v);     //deductabled ddl
            rd.Property(x => x.VehicleMake).HasColumnName("Field23").HasColumnType(v);   //make
            rd.Property(x => x.VehicleModel).HasColumnName("Field24").HasColumnType(v);  //brand
            rd.Property(x => x._Field29).HasColumnName("Field29").HasColumnType(v);
            rd.Property(x => x._A6).HasColumnName("A6");
            rd.Property(x => x._A8).HasColumnName("A8");
            rd.Property(x => x._A10).HasColumnName("A10");
            rd.Property(x => x._A11).HasColumnName("A11");
            rd.Property(x => x._PremiumRate).HasColumnName("A17");
            rd.Property(x => x._A20).HasColumnName("A20");
            rd.Property(x => x._A26).HasColumnName("A26");
            rd.Property(x => x._A36).HasColumnName("A36");
            rd.Property(x => x._A37).HasColumnName("A37");
            rd.Property(x => x._A41).HasColumnName("A41");
        }
    }
}