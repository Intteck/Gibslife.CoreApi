using GibsPro.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.CIP_DataAccess.DataContexts.Configuration
{
    class MarineHullPolicySectionConfig : IEntityTypeConfiguration<MarineHullPolicySection>
    {
        public void Configure(EntityTypeBuilder<MarineHullPolicySection> builder)
        {
            var riskDetail = builder.OwnsOne(p => p.RiskDetail);

            //riskDetail.Property(x => x.VehicleRegNo).HasColumnName("Field1");
            //riskDetail.Property(x => x.VehicleTypeID).HasColumnName("Field2");
            //riskDetail.Property(x => x.StateOfIssue).HasColumnName("Field3");
            //riskDetail.Property(x => x.VehicleColour).HasColumnName("Field4");
            //riskDetail.Property(x => x.ManufactureYear).HasColumnName("Field5");
            //riskDetail.Property(x => x.EngineCapacityHP).HasColumnName("Field6");
            //riskDetail.Property(x => x.EngineNumber).HasColumnName("Field7");
            //riskDetail.Property(x => x.ChasisNumber).HasColumnName("Field8");
            //riskDetail.Property(x => x.NumberOfSeats).HasColumnName("Field9");
            //riskDetail.Property(x => x.VehicleUser).HasColumnName("ContentSection");

            ////riskDetail.Property(x => x.fdgdggs).HasColumnName("Field11");   //tracking
            ////riskDetail.Property(x => x.dsfsdfsfd).HasColumnName("Field12"); //rescue
            //riskDetail.Property(x => x.VehicleUsage).HasColumnName("Field13");
            ////riskDetail.Property(x => x.xcvcxvxcv).HasColumnName("Field14");
            ////riskDetail.Property(x => x.cxvxcvxcv).HasColumnName("Field15");
            ////riskDetail.Property(x => x.xcvxcvxcx).HasColumnName("Field16");
            //riskDetail.Property(x => x.CoverType).HasColumnName("Field17");    //cover type
            ////riskDetail.Property(x => x.asdasdasda).HasColumnName("Field18"); // wax code
            ////riskDetail.Property(x => x.sadadsasda).HasColumnName("Field19"); // endorse option
            ////          // field 20 is special
            //riskDetail.Property(x => x.ExcessBuyBack).HasColumnName("Field21"); //excess details
            ////riskDetail.Property(x => x.Field22).HasColumnName("Field22");     //deductabled ddl
            //riskDetail.Property(x => x.VehicleMake).HasColumnName("Field23");   //make
            //riskDetail.Property(x => x.VehicleModel).HasColumnName("Field24");  //brand
            //riskDetail.Property(x => x.Field25).HasColumnName("Field25"); 
            //riskDetail.Property(x => x.Field26).HasColumnName("Field26");
            //riskDetail.Property(x => x.Field27).HasColumnName("Field27");
            //riskDetail.Property(x => x.Field28).HasColumnName("Field28");
            //riskDetail.Property(x => x.Field29).HasColumnName("Field29");
            //riskDetail.Property(x => x.Field30).HasColumnName("Field30");
            //riskDetail.Property(x => x.A1).HasColumnName("A1");
            //riskDetail.Property(x => x.A2).HasColumnName("A2");
            //riskDetail.Property(x => x.A3).HasColumnName("A3");
            //riskDetail.Property(x => x.A4).HasColumnName("A4");
            //riskDetail.Property(x => x.A5).HasColumnName("A5");
            //riskDetail.Property(x => x.A6).HasColumnName("A6");
            //riskDetail.Property(x => x.A7).HasColumnName("A7");
            //riskDetail.Property(x => x.A8).HasColumnName("A8");
            //riskDetail.Property(x => x.A9).HasColumnName("A9");
            //riskDetail.Property(x => x.A10).HasColumnName("A10");
            //riskDetail.Property(x => x.A11).HasColumnName("A11");
            //riskDetail.Property(x => x.A12).HasColumnName("A12");
            //riskDetail.Property(x => x.A13).HasColumnName("A13");
            //riskDetail.Property(x => x.A14).HasColumnName("A14");
            //riskDetail.Property(x => x.A15).HasColumnName("A15");
            //riskDetail.Property(x => x.A16).HasColumnName("A16");
            //riskDetail.Property(x => x.A17).HasColumnName("A17");
            //riskDetail.Property(x => x.A18).HasColumnName("A18");
            //riskDetail.Property(x => x.A19).HasColumnName("A19");
            //riskDetail.Property(x => x.A20).HasColumnName("A20");
            //riskDetail.Property(x => x.A21).HasColumnName("A21");
            //riskDetail.Property(x => x.A22).HasColumnName("A22");
            //riskDetail.Property(x => x.A23).HasColumnName("A23");
            //riskDetail.Property(x => x.A24).HasColumnName("A24");
            //riskDetail.Property(x => x.A25).HasColumnName("A25");
            //riskDetail.Property(x => x.A26).HasColumnName("A26");
            //riskDetail.Property(x => x.A27).HasColumnName("A27");
            //riskDetail.Property(x => x.A28).HasColumnName("A28");
            //riskDetail.Property(x => x.A29).HasColumnName("A29");
            //riskDetail.Property(x => x.A30).HasColumnName("A30");
            //riskDetail.Property(x => x.A31).HasColumnName("A31");
            //riskDetail.Property(x => x.A32).HasColumnName("A32");
            //riskDetail.Property(x => x.A33).HasColumnName("A33");
            //riskDetail.Property(x => x.A34).HasColumnName("A34");
            //riskDetail.Property(x => x.A35).HasColumnName("A35");
            //riskDetail.Property(x => x.A36).HasColumnName("A36");
            //riskDetail.Property(x => x.A37).HasColumnName("A37");
            //riskDetail.Property(x => x.A38).HasColumnName("A38");
            //riskDetail.Property(x => x.A39).HasColumnName("A39");
            //riskDetail.Property(x => x.A40).HasColumnName("A40");
            //riskDetail.Property(x => x.A41).HasColumnName("A41");
            //riskDetail.Property(x => x.A42).HasColumnName("A42");
            //riskDetail.Property(x => x.A43).HasColumnName("A43");
            //riskDetail.Property(x => x.A44).HasColumnName("A44");
            //riskDetail.Property(x => x.A45).HasColumnName("A45");
            //riskDetail.Property(x => x.A46).HasColumnName("A46");
            //riskDetail.Property(x => x.A47).HasColumnName("A47");
            //riskDetail.Property(x => x.A48).HasColumnName("A48");
            //riskDetail.Property(x => x.A49).HasColumnName("A49");
            //riskDetail.Property(x => x.A50).HasColumnName("A50");
        }
    }
}