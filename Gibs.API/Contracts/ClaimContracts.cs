using System;
using Gibs.Domain.Entities;

namespace Gibs.Api.Contracts.V1
{
    public class RegisterClaimRequest
    {
        public required string PolicyNo { get; init; }
        public required DateOnly LossDate { get; init; }
        public required DateOnly NotifyDate { get; init; }
        public required string Description { get; init; }
        public string? Reference { get; init; }
    }

    public class ClaimResponse(ClaimNotify notify)
    {
        public string NotificationNo { get; } = notify.NotifyNo;
        public string ClaimNo { get; } = notify.NotifyNo;
        public string PolicyNo { get; } = notify.PolicyNo;
        public DateOnly LossDate { get; } = notify.LossDate;
        public DateOnly NotifyDate { get; } = notify.NotifyDate;
        public string Description { get; } = notify.LossDetails;
        public string Status { get; } = notify.Status; // NOTIFIED
                                                       //REGISTERED - AWAITING PAYMENT, PAID
    }
}
