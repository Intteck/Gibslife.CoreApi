using Gibs.Domain.Entities;
using System.Linq;

namespace Gibs.Api.Contracts.V1
{
    public class CreateRegionRequest
    {
        public required string RegionID { get; init; }
        public required string RegionName { get; init; }
        public string? RegionAltName { get; init; }
    }

    public class UpdateRegionRequest
    {
        public required string RegionName { get; init; }
        public string? RegionAltName { get; init; }
    }

    public class RegionResponse(Region region)
    {
        public string RegionID { get; } = region.Id;
        public string RegionName { get; } = region.Name;
        public string? RegionAltName { get; } = region.AltName;
        public BranchResponse[] Branches { get; } 
            = region.Branches.Select(x => new BranchResponse(x)).ToArray();
    }
}
