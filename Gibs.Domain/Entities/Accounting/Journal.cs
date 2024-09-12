using System.ComponentModel.DataAnnotations;

namespace Gibs.Domain.Entities
{
    public class Journal
    {
        [Key]
        public string JournalID { get; private set; }
        [Required]
        public string BranchID { get; private set; }
        [Required]
        public DateTime TransDate { get; private set; }
        [Required]
        public string TransType { get; private set; }
        [Required]
        public string Narration { get; private set; }
        [Required]
        public decimal FxRate { get; private set; }
        [Required]
        public FxCurrency FxCurrency { get; private set; }
        [Required]
        public decimal Amount { get; private set; }
        [Required]
        public ApprovalStatusEnum? Status { get; private set; }

        //Navigation Properties
        public virtual ReadOnlyList<JournalDetail> Details { get; private set; }
    }
}
