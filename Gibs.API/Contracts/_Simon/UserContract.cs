using System;
using System.Linq;
using Gibs.Domain.Entities;

namespace Gibs.Api.Contracts.V1
{
    public class CreateUserRequest
    {
        public required string UserID { get; init; }
        public required string Password { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string Email { get; init; }
        public required string Phone { get; init; }
    }

    public class UpdateUserRequest
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public required string Email { get; init; }
        public required string Phone { get; init; }
        public required bool Active { get; init; }
    }

    public class UpdateUserPasswordRequest
    {
        public required string OldPassword { get; init; }
        public required string NewPassword { get; init; }
    }

    public class UserResponse(User user)
    {
        public string UserID { get; } = user.Id;
        public string FirstName { get; } = user.FirstName;
        public string LastName { get; } = user.LastName;
        public string Email { get; } = user.Email;
        public string Phone { get; } = user.Phone;
        public DateTime? PasswordExpiry { get; } = user.PasswordExpiryUtc;
        public DateTime? LastLogin { get; } = user.LastLoginUtc;
        public string? StaffNo { get; } = user.StaffNo;
        public bool IsActive { get; } = user.IsActive;

        public GroupResponse[]? Groups { get; } 
            = user.Groups?.Select(x => new GroupResponse(x)).ToArray();
        public ApprovalResponse[]? Approvals { get; } 
            = user.Approvals?.Select(x => new ApprovalResponse(x)).ToArray();
        public SignatureResponse[]? Signatures { get; } 
            = user.Signatures?.Select(x => new SignatureResponse(x)).ToArray();


        public DateTime CreatedTime { get; } = user.CreatedUtc;
        public string CreatedUser { get; } = user.CreatedBy;
        public DateTime? ModifiedTime { get; } = user.LastModifiedUtc;
        public string? ModifiedUser { get; } = user.LastModifiedBy;
    }
}
