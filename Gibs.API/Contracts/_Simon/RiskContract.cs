using Gibs.Domain.Entities;
using System.Linq;

namespace Gibs.Api.Contracts.V1
{
    public class CreateMidRiskRequest
    {
        public required string MidRiskID { get; init; }
        public required string MidRiskName { get; init; }
    }

    public class UpdateMidRiskRequest
    {
        public required string MidRiskName { get; init; }
    }

    public class MidRiskResponse(MidClass midRisk)
    {
        public string RiskID { get; } = midRisk.ClassId;
        public string MidRiskID { get; } = midRisk.Id;
        public string MidRiskName { get; } = midRisk.Name;
        public ProductResponse[] Products { get; }
            = midRisk.Products.Select(x => new ProductResponse(x)).ToArray();
    }

    public class RiskResponse(Class risk)
    {
        public string RiskID { get; } = risk.Id;
        public string RiskName { get; } = risk.Name;
        public MidRiskResponse[] MidRisks { get; } 
            = risk.MidClasses.Select(x => new MidRiskResponse(x)).ToArray();
    }

}
