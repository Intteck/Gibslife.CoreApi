using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gibs.Domain.Entities
{
	public class TreatyClaim : AuditRecord
	{

		[Key]
		public long TreatyClaimID { get; set; }

		public string ClaimNo { get; set; }

		public string NoteNo { get; set; }

		public string BrCode { get; set; }

		public string PolicyNo { get; set; }

		public string PartyID { get; set; }

		public string PartyName { get; set; }

		public decimal Amount { get; set; }

		public string LossDetails { get; set; }

		public DateTime TransDate { get; set; }

		public DateTime NotifyDate { get; set; }

		public DateTime LossDate { get; set; }

		public string ClassID { get; set; }

		public string InsuredName { get; set; }

		public string RecType { get; set; }

		public decimal FacAmount { get; set; }

		public decimal CessionRate { get; set; }

		public decimal CessionAmount { get; set; }

		public decimal LossBeforeRate { get; set; }

		public decimal LossBeforeAmount { get; set; }

		public decimal SurpRate { get; set; }

		public decimal SurpAmount { get; set; }

		public decimal Surp2Rate { get; set; }

		public decimal Surp2Amount { get; set; }

		public decimal ExcessLossAmount { get; set; }

		public decimal NetLoss { get; set; }

		public string ApproveBy { get; set; }

		public DateTime ApproveOn { get; set; }

		public string TransGuid { get; set; }

		public string Status { get; set; }

	}
}
