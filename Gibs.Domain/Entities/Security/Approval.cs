
namespace Gibs.Domain.Entities
{
    public class Approval : AuditRecord
    {
        public string UserId { get; set; }
        public ApprovalIdEnum ApprovalId { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }


        //Navigation Properties
        public virtual User User { get; private set; } 

        protected Approval() { /*EfCore*/ }

        public Approval(User user, ApprovalIdEnum approvalId, decimal minValue, decimal maxValue)
        {
            ArgumentNullException.ThrowIfNull(user);

            User = user;
            UserId = user.Id;
            ApprovalId = approvalId;

            UpdateApproval(minValue, maxValue);
        }

        public void UpdateApproval(decimal minValue, decimal maxValue)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(minValue);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(maxValue);
            ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(maxValue, minValue);

            MinValue = minValue;
            MaxValue = maxValue;
        }
    }

    public enum ApprovalIdEnum
    {
        DEBIT_NOTE,
    }
}
