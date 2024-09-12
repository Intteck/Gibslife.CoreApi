using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gibs.Domain.Entities
{
	public class TreatyRaw : AuditRecord
	{

		[Key]
		public long TreatyRawID { get; set; }

		public string NoteNo { get; set; }

		public string BrCode { get; set; }

		public string PolicyNo { get; set; }

		public string InsuredName { get; set; }

		public DateTime BillingDate { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public string TransType { get; set; }

		public string ProductID { get; set; }

		public decimal PremiumRate { get; set; }

		public decimal GrossPremium { get; set; }

		public decimal SumInsured { get; set; }

		public decimal RetPremiumRate { get; set; }

		public decimal RetValue { get; set; }

		public decimal RetPremiumVal { get; set; }

		public decimal Surp1Rate { get; set; }

		public decimal Surp1Value { get; set; }

		public decimal Surp1PremiumValue { get; set; }

		public decimal Surp2Rate { get; set; }

		public decimal Surp2Value { get; set; }

		public decimal Surp2PremiumValue { get; set; }

		public decimal BalFacRate { get; set; }

		public decimal BalFacSumValue { get; set; }

		public decimal BalFacPremiumValue { get; set; }

		public string ApprovedBy { get; set; }

		public DateTime ApprovedOn { get; set; }

		public string CessionNo { get; set; }

		public byte Deleted { get; set; }

		public byte Active { get; set; }

		public byte TreatyItem { get; set; }

		public string Remark { get; set; }
	}
}
