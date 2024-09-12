using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Gibs.Api.Contracts.V1;
using Gibs.Domain.Entities;

namespace Gibs.Api.Controllers
{
    [Route("api/roles")]
    public class RolesController(ControllerServices services) : SecureControllerBase(services)
    {
        [HttpGet("{roleId}")]
        public async Task<RoleResponse> GetRole(
            [FromRoute] string roleId)
        {
            var role = await UoW.Roles.Include(x => x.Permissions)
                                      .Where(x => x.Id == roleId)
                                      .SingleOrDefaultAsync()
               ?? throw new NotFoundException($"Role [{roleId}] was not found");

            return new RoleResponse(role);
        }

        [HttpGet]
        public async Task<IEnumerable<RoleResponse>> ListRoles()
        {
            var query = UoW.Roles.Include(x => x.Permissions)
                                 .OrderBy(x => x.Id)
                                 .AsNoTracking();

            var paging = new PaginationQuery(1000);
            return await PagedList(query, paging, role => new RoleResponse(role));
        }

        [HttpPost]
        public async Task<RoleResponse> CreateRole(
            [FromBody] CreateRoleRequest dto)
        {
            var role = new Role(dto.RoleID, dto.RoleName);
            UoW.Roles.Add(role);

            await UoW.SaveChangesAsync();

            return new RoleResponse(role);
        }

        [HttpPost("{roleId}")]
        public async Task<RoleResponse> UpdateRole(
            [FromRoute] string roleId, 
            [FromBody] UpdateRoleRequest dto)
        {
            var role = await UoW.Roles.FindAsync(roleId)
               ?? throw new NotFoundException($"Role [{roleId}] was not found");

            role.UpdateRoleName(dto.RoleName);
            await UoW.SaveChangesAsync();

            return new RoleResponse(role);
        }

        [HttpPost("{roleId}/permissions")]
        public async Task<RoleResponse> UpdateRolePermissions(
            [FromRoute] string roleId, 
            [FromBody] string[] permissionIds)
        {
            var role = await UoW.Roles.FindAsync(roleId)
               ?? throw new NotFoundException($"Role [{roleId}] was not found");

            var permissions = await UoW.Permissions.Where(x => permissionIds.Contains(x.Id)).ToListAsync();
            role.ReplacePermissions(permissions);

            await UoW.SaveChangesAsync();

            return new RoleResponse(role);
        }

        [HttpDelete("{roleId}")]
        public async Task DeleteRole(
            [FromRoute] string roleId)
        {
            var role = await UoW.Roles.FindAsync(roleId)
               ?? throw new NotFoundException($"Role [{roleId}] was not found");

            UoW.Remove(role);
            await UoW.SaveChangesAsync();
        }
    }

    [Route("api/permissions")]
    public class PermissionController(ControllerServices services) : SecureControllerBase(services)
    {
        [HttpGet]
        public async Task<IEnumerable<PermissionResponse>> ListPermissions(
           [FromQuery] PaginationQuery paging)
        {
            var query = UoW.Permissions.Include(x => x.Roles)
                                       .OrderBy(x => x.Id)
                                       .AsNoTracking();

            return await PagedList(query, paging, permission => new PermissionResponse(permission));
        }
    }
}
