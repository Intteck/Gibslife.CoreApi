using System.Linq;
using Gibs.Domain.Entities;

namespace Gibs.Api.Contracts.V1
{
    public class CreateRoleRequest
    {
        public required string RoleID { get; init; }
        public required string RoleName { get; init; }
    }

    public class UpdateRoleRequest
    {
        public required string RoleName { get; init; }
    }

    public class RoleResponse(Role role)
    {
        public string RoleID { get; } = role.Id;
        public string RoleName { get; } = role.Name;

        //public UserResponse[]? Users { get; }
        public GroupResponse[]? Groups { get; } //= role.Groups?.Select(x => new GroupResponse(x)).ToArray();
        public PermissionResponse[]? Permissions { get; } 
            = role.Permissions?.Select(x => new PermissionResponse(x)).ToArray();
    }

    public class PermissionResponse(Permission permission)
    {
        public string PermissionID { get; } = permission.Id;
        public string PermissionName { get; } = permission.Name;
    }
}
