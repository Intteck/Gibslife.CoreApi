using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Gibs.Api.Contracts.V1;
using Gibs.Domain.Entities;

namespace Gibs.Api.Controllers
{
    [Route("api/users")]
    public class UsersController(ControllerServices services) : SecureControllerBase(services)
    {
        [HttpGet("online")]
        public async Task<IEnumerable<UserResponse>> ListUsersOnline()
        {
            var query = UoW.Users.Include(x => x.Groups)
                                 .OrderBy(x => x.Id)
                                 .Where(x=> x.LastLoginUtc > DateTime.Now.AddDays(-1))
                                 .AsNoTracking();

            var paging = new PaginationQuery(1000);
            return await PagedList(query, paging, user => new UserResponse(user));
        }

        [HttpGet]
        public async Task<IEnumerable<UserResponse>> ListUsers(
            [FromQuery] PaginationQuery paging,
            [FromQuery] StringSearchQuery search)
        {
            var query = UoW.Users//.Include(x=> x.Groups)
                                 .OrderBy(x => x.Id)
                                 .AsNoTracking();

            if (search.CanSearch)
                query = query.Where(x => x.Id.StartsWith(search.SearchText)
                                      || x.FirstName.StartsWith(search.SearchText)
                                      || x.LastName.StartsWith(search.SearchText));

            //if (!string.IsNullOrWhiteSpace(createdUser))
            //    query = query.Where(x => x.CreatedBy.StartsWith(createdUser));

            //if (!string.IsNullOrWhiteSpace(modifiedUser))
            //    query = query.Where(x => x.LastModifiedBy.StartsWith(modifiedUser));

            //if (createdTime != null)
            //    query = query.Where(x => x.CreatedUtc == createdTime);

            //if (modifiedTime != null)
            //    query = query.Where(x => x.LastModifiedUtc == modifiedTime);

            return await PagedList(query, paging, user => new UserResponse(user));
        }

        [HttpGet("{userId}")]
        public async Task<UserResponse> GetUser(
            [FromRoute] string userId)
        {
            var user = await UoW.Users.Include(x => x.Groups)
                                      .Include(x => x.Approvals)
                                      .Include(x => x.Signatures)
                                      .Where(x => x.Id == userId)
                                      .SingleOrDefaultAsync()
               ?? throw new NotFoundException($"User [{userId}] was not found");

            return new UserResponse(user);
        }

        [HttpPost]
        public async Task<UserResponse> CreateUser(
            [FromBody] CreateUserRequest dto)
        {
            var user = new User(dto.UserID, dto.Password, dto.FirstName, dto.LastName, dto.Phone, dto.Email);
            UoW.Users.Add(user);

            await UoW.SaveChangesAsync();

            return new UserResponse(user);
        }

        [HttpPost("{userId}")]
        public async Task<UserResponse> UpdateUser(
            [FromRoute] string userId, 
            [FromBody] UpdateUserRequest dto)
        {
            var user = await UoW.Users.FindAsync(userId)
               ?? throw new NotFoundException($"User [{userId}] was not found");

            user.UpdateProfile(dto.FirstName, dto.LastName, dto.Phone);
            user.UpdateStatus(dto.Active);

            await UoW.SaveChangesAsync();

            return new UserResponse(user);
        }

        [HttpPost("{userId}/password")]
        public async Task<UserResponse> UpdateUserPassword(
            [FromRoute] string userId, 
            [FromBody] UpdateUserPasswordRequest dto)
        {
            var user = await UoW.Users.FindAsync(userId)
               ?? throw new NotFoundException($"User [{userId}] was not found");

            user.ChangePassword(dto.OldPassword, dto.NewPassword, 90);

            await UoW.SaveChangesAsync();
            return new UserResponse(user);
        }

        [HttpPost("{userId}/groups")]
        public async Task<UserResponse> UpdateUserGroups(
            [FromRoute] string userId,
            [FromBody] string[] groupIds)
        {
            var user = await UoW.Users.FindAsync(userId)
               ?? throw new NotFoundException($"User [{userId}] was not found");

            var groups = await UoW.Groups.Where(x => groupIds.Contains(x.Id)).ToListAsync();
            user.UpdateGroups(groups);

            await UoW.SaveChangesAsync();

            return new UserResponse(user);
        }

        [HttpDelete("{userId}")]
        public async Task DeleteUser(
            [FromRoute] string userId)
        {
            var user = await UoW.Users.FindAsync(userId)
               ?? throw new NotFoundException($"User [{userId}] was not found");

            UoW.Remove(user);
            await UoW.SaveChangesAsync();
        }
    }
}
