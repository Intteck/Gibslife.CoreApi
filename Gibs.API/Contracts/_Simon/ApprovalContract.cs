using System;
using Gibs.Domain.Entities;

namespace Gibs.Api.Contracts.V1
{
    public class CreateApprovalRequest
    {
        public required string ApprovalID { get; init; }
        public required decimal MinValue { get; init; }
        public required decimal MaxValue { get; init; }
    }

    public class UpdateApprovalRequest
    {
        public required decimal MinValue { get; init; }
        public required decimal MaxValue { get; init; }
    }

    public class ApprovalResponse(Approval approval)
    {
        public string UserID { get; } = approval.UserId;
        public string ApprovalID { get; } = approval.ApprovalId.ToString();

        public decimal MinValue { get; } = approval.MinValue;
        public decimal MaxValue { get; } = approval.MaxValue;

        public string CreatedBy { get; } = approval.CreatedBy;
        public DateTime CreatedOn { get; } = approval.CreatedUtc;
        public string? UpdatedBy { get; } = approval.LastModifiedBy;
        public DateTime? UpdateOn { get; } = approval.LastModifiedUtc;
    }
}
