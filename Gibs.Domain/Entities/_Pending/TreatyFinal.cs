using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gibs.Domain.Entities
{
	public class TreatyFinal : AuditRecord
	{

		[Key]
		public long TreatyFinalID { get; set; }

		public string NoteNo { get; set; }

		public string PolicyNo { get; set; }

		public string PartyID { get; set; }

		public string PartyName { get; set; }

		public string BranchID { get; set; }
		public string ProductID { get; set; }

		public string ClassID { get; set; }
		public string ClassName { get; set; }

		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public DateTime TransDate { get; set; }

		public decimal SumInsured { get; set; }

		public decimal GrossPremium { get; set; }

		public decimal PremiumRate { get; set; }

		public string UnderYear { get; set; }

		public decimal FacRate { get; set; }
		public decimal FacPremium { get; set; }

		public string CessionNo { get; set; }

		public decimal RetentionRate { get; set; }
		public decimal RetentionValue { get; set; }
		public decimal RetentionPremValue { get; set; }

		public decimal EOLRate { get; set; }

		public decimal BalFacSumAmount { get; set; }

		public decimal MPLRate { get; set; }
		public decimal MPLValue { get; set; }
		public decimal MPLPremium { get; set; }


		public decimal SurpRate { get; set; }
		public decimal Surp2Rate { get; set; }

		public decimal SurpValue { get; set; }
		public decimal Surp2Value { get; set; }

		public decimal SurpPremiumValue { get; set; }
		public decimal Surp2PremiumValue { get; set; }

		public byte Deleted { get; set; }

		public byte Active { get; set; }

		public byte TreatyItem { get; set; }

		public string Narration { get; set; }

		public string Remark { get; set; }

		public string Tag { get; set; }

	}
}
