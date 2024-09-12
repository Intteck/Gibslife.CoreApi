using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gibs.Domain.Entities
{
    public class Voucher
    {
        [Key]
        public string VoucherID { get; private set; }
        [Required]
        public string BranchID { get; private set; }
        [Required]
        public DateTime TransDate { get; private set; }
        [Required]
        public string VoucherType { get; private set; } //RECIEPT, PAYMENT
        [Required]
        public string PaymentType { get; private set; } //
        public string Narration { get; private set; }

        public string TotalType { get; private set; } //GROSS, NET
        public string PartPaymentType { get; private set; } //FULL, PART, BALANCE

        public string ChequeNo { get; private set; }
        public string ChequeBank { get; private set; }
        public DateTime ChequeDate { get; private set; }

        public string LodgementName { get; private set; }
        public string LodgementBank { get; private set; }
        public DateTime LodgementDate { get; private set; }

        [Required]
        public decimal FxRate { get; private set; }
        [Required]
        public FxCurrency FxCurrency { get; private set; }
        [Required]
        public decimal Amount { get; private set; }
        [Required]
        public ApprovalEnum? ApprovalStatus { get; protected set; }

        //Navigation Properties
        public virtual ReadOnlyList<VoucherNote> VoucherNotes { get; private set; }
        public virtual ReadOnlyList<VoucherDeduction> VoucherDeductions { get; private set; }
    }

    public class VoucherNote
    {
        [Key, Column(Order = 0)]
        public string VoucherID { get; private set; }
        [Key, Column(Order = 1)]
        public string NoteID { get; private set; }
        [Required]
        public decimal Amount { get; private set; }

        //Navigation Properties
    }

    public class VoucherDeduction
    {
        [Key, Column(Order = 0)]
        public string VoucherID { get; private set; }
        [Key, Column(Order = 1)]
        public string DeductionID { get; private set; }
        [Required]
        public Account LedgerAccount { get; private set; }
        [Required]
        public decimal Rate { get; private set; }
        [Required]
        public decimal Amount { get; private set; }

        public string Narration { get; private set; }

        //Navigation Properties
    }
}
