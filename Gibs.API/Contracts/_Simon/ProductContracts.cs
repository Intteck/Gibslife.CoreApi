using Gibs.Domain.Entities;
using System.Linq;

namespace Gibs.Api.Contracts.V1
{
    public class CreateProductRequest
    {
        public required string RiskID { get; init; }
        public string? MidRiskID { get; init; }
        public required string ProductID { get; init; }
        public required string ProductName { get; init; }
        public string? AlternateName { get; init; }
        public string? NaicomID { get; init; }
    }

    public class UpdateProductRequest
    {
        public required string ProductName { get; init; }
    }

    public class ProductResponse(Product product)
    {
        public string RiskID { get; } = product.ClassId;
        public string? MidRiskID { get; } = product.MidClassId;
        public string ProductID { get; } = product.Id;
        public string ProductName { get; } = product.Name;
        public string? AlternateName { get; } = product.AltName;
        public string? NaicomID { get; } = product.NaicomCode;
        public SectionResponse[] Sections { get; } 
            = product.Sections.Select(x => new SectionResponse(x)).ToArray();
        public FieldResponse[] Fields { get; }
            = product.GetVisibleFields().Select(x => new FieldResponse(x)).ToArray();
        public RateResponse[] Rates { get; } = [];
    }

    public class SectionResponse
    {
        public SectionResponse(ProductSection section)
        {
            SectionID = section.SectionId;
            SectionName = section.SectionName;
            SMIs = section.SMIs.Select(x => new SmiResponse(x)).ToArray();
        }
        public string SectionID { get; }
        public string SectionName { get; }
        public FieldResponse[]? Fields { get; }
        public RateResponse[]? Rates { get; }
        public SmiResponse[] SMIs { get; }
    }

    public class FieldResponse(ProductField field)
    {
        public string Name { get; } = field.FieldId;
        public string Type { get; } = field.FieldTypeDescription;
        public bool Required { get; } = field.IsRequired;
        public string? Description { get; } = field.Description;

        //public string DefaultValue { get; }
        //public string MaximumValue { get; }
        //public string MinimumValue { get; }
    }

    public class RateResponse(ProductField field)
    {
        public string Name { get; } = field.FieldId;
        public string Type { get; } = field.FieldTypeDescription;
        public bool Required { get; } = field.IsRequired;
        public string? Description { get; } = field.Description;

        public decimal DefaultValue { get; }
        public decimal MaximumValue { get; }
        public decimal MinimumValue { get; }
    }

    public class SmiResponse(ProductSMI smi)
    {
        public string Code { get; } = smi.SmiId;
        public string Name { get; } = smi.SmiName;

        // staging features
        //public bool Required { get; }
        //public decimal DefaultRate { get; }
        //public decimal MaximumRate { get; }
        //public decimal MinimumRate { get; }
    }

}
