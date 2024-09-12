
namespace Gibs.Domain.Entities
{
    public abstract class ApprovalRecord : AuditRecord
    {
        /*[Column(Order = 804)]*/ public string? ApprovedBy { get; protected set; }
        /*[Column(Order = 805)]*/ public DateTime? ApprovedUtc { get; protected set; }
        /*[Column(Order = 806)]*/ public ApprovalEnum? ApprovalStatus { get; protected set; }

        public ApprovalRecord()
        {
            ApprovalStatus = ApprovalEnum.PENDING;
        }

        internal void Approve(User approver)
        {
            ArgumentNullException.ThrowIfNull(approver);

            ApprovalStatus = ApprovalEnum.APPROVED;
            ApprovedUtc = DateTime.UtcNow;
            ApprovedBy = approver.Id;
        }
    }

    public abstract class AuditRecord
    {
        /*[Column(Order = 800)]*/ public string CreatedBy { get; private set; }
        /*[Column(Order = 801)]*/ public DateTime CreatedUtc { get; private set; } = DateTime.UtcNow;
        /*[Column(Order = 802)]*/ public string? LastModifiedBy { get; private set; }
        /*[Column(Order = 803)]*/ public DateTime? LastModifiedUtc { get; private set; }
    }
}