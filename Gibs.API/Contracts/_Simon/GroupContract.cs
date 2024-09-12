using System.Linq;
using Gibs.Domain.Entities;

namespace Gibs.Api.Contracts.V1
{
    public class CreateGroupRequest
    {
        public required string GroupID { get; init; }
        public required string GroupName { get; init; }
    }

    public class UpdateGroupRequest
    {
        public required string GroupName { get; init; }
    }

    public class GroupResponse(Group group)
    {
        public string GroupID { get; } = group.Id;
        public string GroupName { get; } = group.Name;
        public string[]? Permissions { get; } = group.Users?.Select(x => x.Id).ToArray();
    }
}
