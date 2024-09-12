using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Gibs.Api.Contracts.V1;
using Gibs.Domain.Entities;

namespace Gibs.Api.Controllers
{
    [Route("api/groups")]
    public class GroupsController(ControllerServices services) : SecureControllerBase(services)
    {
        //[HttpGet("{groupId}")]
        //public async Task<GroupResponse> GetGroup(string groupId)
        //{
        //    var group = await UoW.Groups.Include(x => x.Users)
        //                               .Where(x => x.Id == groupId)
        //                               .SingleOrDefaultAsync()
        //       ?? throw new NotFoundException($"Group [{groupId}] was not found");

        //    return new GroupResponse(group);
        //}

        [HttpGet]
        public async Task<IEnumerable<GroupResponse>> ListGroups()
        {
            var query = UoW.Groups.Include(x => x.Users)
                                 .OrderBy(x => x.Id)
                                 .AsNoTracking();

            var paging = new PaginationQuery(200);
            return await PagedList(query, paging, group => new GroupResponse(group));
        }

        [HttpPost]
        public async Task<GroupResponse> CreateGroup(CreateGroupRequest dto)
        {
            var group = new Group(dto.GroupID, dto.GroupName);
            UoW.Groups.Add(group);

            await UoW.SaveChangesAsync();

            return new GroupResponse(group);
        }

        [HttpPost("{groupId}")]
        public async Task<GroupResponse> UpdateGroup(string groupId, UpdateGroupRequest dto)
        {
            var group = await UoW.Groups.FindAsync(groupId)
               ?? throw new NotFoundException($"Group [{groupId}] was not found");

            group.UpdateGroupName(dto.GroupName);
            await UoW.SaveChangesAsync();

            return new GroupResponse(group);
        }

        [HttpPost("{groupId}/users")]
        public async Task<GroupResponse> UpdateGroupUsers(string groupId, string[] userIds)
        {
            var group = await UoW.Groups.FindAsync(groupId)
               ?? throw new NotFoundException($"Group [{groupId}] was not found");

            var users = await UoW.Users.Where(x => userIds.Contains(x.Id)).ToListAsync();
            group.ReplaceUsers(users);

            await UoW.SaveChangesAsync();

            return new GroupResponse(group);
        }

        [HttpDelete("{groupId}")]
        public async Task DeleteGroup(string groupId)
        {
            var group = await UoW.Roles.FindAsync(groupId)
               ?? throw new NotFoundException($"Group [{groupId}] was not found");

            UoW.Remove(group);
            await UoW.SaveChangesAsync();
        }
    }
}
