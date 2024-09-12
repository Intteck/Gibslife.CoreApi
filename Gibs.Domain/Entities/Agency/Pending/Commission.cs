using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gibs.Domain.Entities
{
    public class Commission : AuditRecord
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BatchID { get; private set; }
        [Required]
        public string PartyID { get; private set; }
        [Required]
        public DateTime TransDate { get; private set; }
        [Required]
        public DateTime StartDate { get; private set; }
        [Required]
        public DateTime EndDate { get; private set; }

        public decimal GrossPremium { get; private set; }
        public decimal PaidPremium { get; private set; }
        public decimal CommAmount { get; private set; }
        public decimal VatAmount { get; private set; }
        public decimal NetAmount { get; private set; }
        public ApprovalEnum? ApprovalStatus { get; protected set; }

        //Navigation Properties
        public virtual Party Party { get; protected set; }
        public virtual ReadOnlyList<CommissionDetail> Details { get; private set; }
    }

    public class CommissionDetail
    {
		[Key, Column(Order = 0)]
        public string BatchID { get; private set; }
		[Key, Column(Order = 1)]
        public string NoteID { get; private set; }

        public decimal GrossPremium { get; private set; }

        //Navigation Properties 
        public virtual Commission Commission { get; protected set; }
        public virtual DebitNote Note { get; protected set; }
    }
}
