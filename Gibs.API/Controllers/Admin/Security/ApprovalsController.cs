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
    [Route("api/approvals")]
    public class ApprovalsController(ControllerServices services) : SecureControllerBase(services)
    {
        [HttpGet]
        public async Task<IEnumerable<ApprovalResponse>> ListApprovals(
            [FromQuery] PaginationQuery paging)
        {
            var query = UoW.Approvals.OrderBy(x => x.UserId)
                                     .ThenBy(x => x.ApprovalId)
                                     .AsNoTracking();

            return await PagedList(query, paging, approval => new ApprovalResponse(approval));
        }
    }

    [Route("api/users/{userId}/approvals")]
    public class UserApprovalsController(ControllerServices services) : SecureControllerBase(services)
    {
        [HttpGet]
        public async Task<IEnumerable<ApprovalResponse>> ListApprovals(
            [FromRoute] string userId)
        {
            var query = UoW.Approvals.Where(x => x.UserId == userId)
                                     .OrderBy(x => x.ApprovalId)
                                     .AsNoTracking();

            var paging = new PaginationQuery(200);
            return await PagedList(query, paging, approval => new ApprovalResponse(approval));
        }

        [HttpPost]
        public async Task<ApprovalResponse> CreateApproval(
            [FromRoute] string userId,
            [FromBody] CreateApprovalRequest dto)
        {
            var user = await UoW.Users.FindAsync(userId)
               ?? throw new NotFoundException($"User [{userId}] was not found");

            if (!Enum.TryParse(dto.ApprovalID, out ApprovalIdEnum approverId))
               throw new NotFoundException($"ApprovalID [{dto.ApprovalID}] was not found");

            var approval = new Approval(user, approverId, dto.MinValue, dto.MaxValue);
            UoW.Approvals.Add(approval);

            await UoW.SaveChangesAsync();

            return new ApprovalResponse(approval);
        }

        [HttpPost("{approvalId}")]
        public async Task<ApprovalResponse> UpdateApproval(
            [FromRoute] string userId,
            [FromRoute] string approvalId,
            [FromBody] UpdateApprovalRequest dto)
        {
            if (Enum.TryParse(approvalId, out ApprovalIdEnum result))
            {
                var approval = await UoW.Approvals.FindAsync(userId, result)
                   ?? throw new NotFoundException($"Approval [{userId}, {approvalId}] was not found");

                approval.UpdateApproval(dto.MinValue, dto.MaxValue);
                await UoW.SaveChangesAsync();

                return new ApprovalResponse(approval);
            }
            throw new ArgumentException($"Invalid ApprovalID [{approvalId}]", nameof(approvalId));
        }

        [HttpDelete("{approvalId}")]
        public async Task DeleteApproval(
            [FromRoute] string userId, 
            [FromRoute] string approvalId)
        {
            if (Enum.TryParse(approvalId, out ApprovalIdEnum result))
            {
                var approval = await UoW.Approvals.FindAsync(userId, result)
                   ?? throw new NotFoundException($"Approval [{userId}, {approvalId}] was not found");

                UoW.Remove(approval);
                await UoW.SaveChangesAsync();
                return;
            }
            throw new ArgumentException($"Invalid ApprovalID [{approvalId}]", nameof(approvalId));
        }
    }
}
