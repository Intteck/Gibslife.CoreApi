using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class PolicySectionConfig : IEntityTypeConfiguration<PolicySection>
    {
        public void Configure(EntityTypeBuilder<PolicySection> builder)
        {
            builder.ToTable("Sections", "policy")
                   .HasKey(x => new { x.DeclareNo, x.SectionId });

            builder.HasOne(x => x.History)
                   .WithMany(x => x.Sections)
                   .HasForeignKey(x => x.DeclareNo);

            builder.HasMany(x => x.SMIs).WithOne()
                   .HasForeignKey(x => new { x.DeclareNo, x.SectionId });

            builder.HasOne<ProductSection>()
                   .WithMany()
                   .HasForeignKey(x => new { x.ProductId, x.SectionId });

            builder.Property(x => x.SerialNo).HasColumnName("SN").ValueGeneratedOnAdd(); 
            builder.Property(x => x.DeclareNo).HasColumnName("DeclareNo");
            builder.Property(x => x.ProductId).HasColumnName("ProductID");
            builder.Property(x => x.SectionId).HasColumnName("SectionID");
            builder.Property(x => x.CertificateNo).HasColumnName("CertificateNo");

            builder.Property(x => x.ItemSumInsured).HasColumnName("SumInsured");
            builder.Property(x => x.ItemPremium).HasColumnName("Premium");

            builder.Property(x => x.CreatedBy).HasColumnName("CreatedBy");
            builder.Property(x => x.CreatedUtc).HasColumnName("CreatedUtc");
            builder.Property(x => x.LastModifiedBy).HasColumnName("LastModifiedBy");
            builder.Property(x => x.LastModifiedUtc).HasColumnName("LastModifiedUtc");

            //builder.Property(x => x.ClassID).HasColumnName("Field30"); 
            //-//builder.Property("_TransGUID").HasColumnName("TransGUID");
            //builder.Property(x => x.StartDate).HasColumnName("TStartDate");
            //builder.Property(x => x.EndDate).HasColumnName("TEndDate");

            //hacks.Property(x => x.Location).HasColumnName("Location");
            //hacks.Property(x => x.ContentSection).HasColumnName("ContentSection");
            //hacks.Property(x => x.EndorsementType).HasColumnName("Tag");
            //hacks.Property(x => x.OtherSum).HasColumnName("OtherSum");

            var fields = builder.OwnsOne(x => x.ExFields);
            {
                fields.Property(x => x.Field1).HasColumnName("Field1");
                fields.Property(x => x.Field2).HasColumnName("Field2");
                fields.Property(x => x.Field3).HasColumnName("Field3");
                fields.Property(x => x.Field4).HasColumnName("Field4");
                fields.Property(x => x.Field5).HasColumnName("Field5");
                fields.Property(x => x.Field6).HasColumnName("Field6");
                fields.Property(x => x.Field7).HasColumnName("Field7");
                fields.Property(x => x.Field8).HasColumnName("Field8");
                fields.Property(x => x.Field9).HasColumnName("Field9");
                fields.Property(x => x.Field10).HasColumnName("Field10");
                fields.Property(x => x.Field11).HasColumnName("Field11");
                fields.Property(x => x.Field12).HasColumnName("Field12");
                fields.Property(x => x.Field13).HasColumnName("Field13");
                fields.Property(x => x.Field14).HasColumnName("Field14");
                fields.Property(x => x.Field15).HasColumnName("Field15");
                fields.Property(x => x.Field16).HasColumnName("Field16");
                fields.Property(x => x.Field17).HasColumnName("Field17");
                fields.Property(x => x.Field18).HasColumnName("Field18");
                fields.Property(x => x.Field19).HasColumnName("Field19");     
                fields.Property(x => x.Field20).HasColumnName("Field20");
                fields.Property(x => x.Field21).HasColumnName("Field21");
                fields.Property(x => x.Field22).HasColumnName("Field22");
                fields.Property(x => x.Field23).HasColumnName("Field23");
                fields.Property(x => x.Field24).HasColumnName("Field24");
                fields.Property(x => x.Field25).HasColumnName("Field25");
                fields.Property(x => x.Field26).HasColumnName("Field26");
                fields.Property(x => x.Field27).HasColumnName("Field27");
                fields.Property(x => x.Field28).HasColumnName("Field28");
                fields.Property(x => x.Field29).HasColumnName("Field29");
                fields.Property(x => x.Field30).HasColumnName("Field30");

                //hacks.Ignore(x => x.Field30); //Field30 is ClassID
                //hacks.Ignore(x => x.Field20); //Field20 is DeclarationNo

                fields.Ignore(x => x.Field31);
                fields.Ignore(x => x.Field32);
                fields.Ignore(x => x.Field33);
                fields.Ignore(x => x.Field34);
                fields.Ignore(x => x.Field35);
                fields.Ignore(x => x.Field36);
                fields.Ignore(x => x.Field37);
                fields.Ignore(x => x.Field38);
                fields.Ignore(x => x.Field39);
                fields.Ignore(x => x.Field40);
                fields.Ignore(x => x.Field41);
                fields.Ignore(x => x.Field42);
                fields.Ignore(x => x.Field43);
                fields.Ignore(x => x.Field44);
                fields.Ignore(x => x.Field45);
                fields.Ignore(x => x.Field46);
                fields.Ignore(x => x.Field47);
                fields.Ignore(x => x.Field48);
                fields.Ignore(x => x.Field49);
                fields.Ignore(x => x.Field50);

                fields.Property(x => x.A1).HasColumnName("A1");
                fields.Property(x => x.A2).HasColumnName("A2");
                fields.Property(x => x.A3).HasColumnName("A3");
                fields.Property(x => x.A4).HasColumnName("A4");
                fields.Property(x => x.A5).HasColumnName("A5");
                fields.Property(x => x.A6).HasColumnName("A6");
                fields.Property(x => x.A7).HasColumnName("A7");
                fields.Property(x => x.A8).HasColumnName("A8");
                fields.Property(x => x.A9).HasColumnName("A9");
                fields.Property(x => x.A10).HasColumnName("A10");
                fields.Property(x => x.A11).HasColumnName("A11");
                fields.Property(x => x.A12).HasColumnName("A12");
                fields.Property(x => x.A13).HasColumnName("A13");
                fields.Property(x => x.A14).HasColumnName("A14");
                fields.Property(x => x.A15).HasColumnName("A15");
                fields.Property(x => x.A16).HasColumnName("A16");
                fields.Property(x => x.A17).HasColumnName("A17");
                fields.Property(x => x.A18).HasColumnName("A18");
                fields.Property(x => x.A19).HasColumnName("A19");
                fields.Property(x => x.A20).HasColumnName("A20");
                fields.Property(x => x.A21).HasColumnName("A21");
                fields.Property(x => x.A22).HasColumnName("A22");
                fields.Property(x => x.A23).HasColumnName("A23");
                fields.Property(x => x.A24).HasColumnName("A24");
                fields.Property(x => x.A25).HasColumnName("A25");
                fields.Property(x => x.A26).HasColumnName("A26");
                fields.Property(x => x.A27).HasColumnName("A27");
                fields.Property(x => x.A28).HasColumnName("A28");
                fields.Property(x => x.A29).HasColumnName("A29");
                fields.Property(x => x.A30).HasColumnName("A30");
                fields.Property(x => x.A31).HasColumnName("A31");
                fields.Property(x => x.A32).HasColumnName("A32");
                fields.Property(x => x.A33).HasColumnName("A33");
                fields.Property(x => x.A34).HasColumnName("A34");
                fields.Property(x => x.A35).HasColumnName("A35");
                fields.Property(x => x.A36).HasColumnName("A36");
                fields.Property(x => x.A37).HasColumnName("A37");
                fields.Property(x => x.A38).HasColumnName("A38");
                fields.Property(x => x.A39).HasColumnName("A39");
                fields.Property(x => x.A40).HasColumnName("A40");
                fields.Property(x => x.A41).HasColumnName("A41");
                fields.Property(x => x.A42).HasColumnName("A42");
                fields.Property(x => x.A43).HasColumnName("A43");
                fields.Property(x => x.A44).HasColumnName("A44");
                fields.Property(x => x.A45).HasColumnName("A45");
                fields.Property(x => x.A46).HasColumnName("A46");
                fields.Property(x => x.A47).HasColumnName("A47");
                fields.Property(x => x.A48).HasColumnName("A48");
                fields.Property(x => x.A49).HasColumnName("A49");
                fields.Property(x => x.A50).HasColumnName("A50");
            }
        }
    }
}