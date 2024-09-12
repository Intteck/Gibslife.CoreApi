using System.ComponentModel.DataAnnotations;

namespace Gibs.Domain.Entities
{
    public class ClaimNotify : ApprovalRecord
    {
        public ClaimNotify() { }

		public ClaimNotify(ICodeNumberFactory codeFactory, DebitNote dn, 
			DateOnly notifyDate, DateOnly lossDate, string lossDetails)
        {
			Members = dn.History.Policy.Members.ShallowCopy();
			Business = dn.History.Business.ShallowCopy();
			//Secure = new ApprovalRecord();

			NotifyNo = codeFactory.CreateCodeNumber(CodeTypeEnum.CLAIM_NOTIFY, null);

			PolicyNo = dn.PolicyNo;
			RefDebitNoteNo = dn.DebitNoteNo;
			UnderwritenYear = dn.History.Business.StartDate.Year;

			SumInsured = dn.History.Premium.SumInsured;
			GrossPremium = dn.History.Premium.GrossPremium;

			Outstanding = 0; //fetch the outstanding
			RefReceiptNo = null;
			Status = "PENDING";


			NotifyDate = notifyDate;
			LossDate = lossDate;
			LossDetails = lossDetails;
			EntryDate = DateTime.Now;

		}

        public string NotifyNo { get; private set; }
        public string? ClaimNo { get; private set; }


        public DateTime EntryDate { get; private set; }
        public string PolicyNo { get; private set; }
        public string RefDebitNoteNo { get; private set; }
        public int UnderwritenYear { get; private set; }


        public DateOnly NotifyDate { get; private set; }
        public DateOnly LossDate { get; private set; }
        public string LossDetails { get; private set; }




        public decimal SumInsured { get; private set; }

        public decimal GrossPremium { get; private set; }

        public decimal Outstanding { get; private set; }

        public string? RefReceiptNo { get; private set; }
        [Required]
        public string Status { get; private set; } = "PENDING";


        public DebitNote DebitNote { get; private set; }

        public BusinessMembers Members { get; private set; }
        public BusinessBasics Business { get; private set; }
        //public ApprovalRecord Secure { get; private set; }
    }
}
