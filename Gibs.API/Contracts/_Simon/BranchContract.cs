using Gibs.Domain.Entities;

namespace Gibs.Api.Contracts.V1
{
    public class CreateBranchRequest
    {
        public required string BranchID { get; init; }
        //public required string RegionID { get; init; }
        public required string StateID { get; init; }
        public required string BranchName { get; init; }
        public string? BranchAltName { get; init; }
        public string? Address { get; init; }
    }

    public class UpdateBranchRequest
    {
        public required string BranchName { get; init; }
        public string? BranchAltName { get; init; }
        public string? Address { get; init; }
    }

    public class BranchResponse(Branch branch)
    {
        public string BranchID { get; } = branch.Id;
        //public string? RegionID { get; } = branch.RegionId;
        
        //public string? RegionName { get; } = branch.RegionName;
        public string StateID { get; } = branch.StateId ?? string.Empty;
        public string BranchName { get; } = branch.Name;
        public string? BranchAltName { get; } = branch.AltName;
        public string? Address { get; } = branch.Address;
    }
}
