using Gibs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    class ClauseConfig : IEntityTypeConfiguration<Clause>
    {
        public void Configure(EntityTypeBuilder<Clause> builder)
        {
            builder.ToTable("Clauses", "policy")
                   .HasKey(x => x.Id);

            builder.HasDiscriminator<string>("ClauseTypeID")
                   .HasValue<MarineClause>("MARINE")
                   .HasValue<MotorClause>("MOTOR")
                   .HasValue<MemoClause>("MEMO");

            builder.Property(x => x.Id).HasColumnName("ClauseID");
            builder.Property(x => x.ProductId).HasColumnName("ProductID");
            builder.Property(x => x.Category).HasColumnName("Category");
        }
    }

    class MemoClauseConfig : IEntityTypeConfiguration<MemoClause>
    {
        public void Configure(EntityTypeBuilder<MemoClause> builder)
        {
            builder.Property(x => x.HeaderText).HasColumnName("Field1");
            builder.Property(x => x.Document1).HasColumnName("Field2");
            builder.Property(x => x.Document2).HasColumnName("Field3");
        }
    }

    class MarineClauseConfig : IEntityTypeConfiguration<MarineClause>
    {
        public void Configure(EntityTypeBuilder<MarineClause> builder)
        {
            builder.Property(x => x.Details).HasColumnName("Field1");
            builder.Property(x => x.ICC).HasColumnName("Field2");
        }
    }

    class MotorClauseConfig : IEntityTypeConfiguration<MotorClause>
    {
        public void Configure(EntityTypeBuilder<MotorClause> builder)
        {
            builder.Property(x => x.EntitledToA).HasColumnName("Field1");
            builder.Property(x => x.EntitledToB).HasColumnName("Field2");
            builder.Property(x => x.LimitationA).HasColumnName("Field3");
            builder.Property(x => x.LimitationB).HasColumnName("Field4");
            builder.Property(x => x.VehicleUsage).HasColumnName("Field5");
        }
    }

}