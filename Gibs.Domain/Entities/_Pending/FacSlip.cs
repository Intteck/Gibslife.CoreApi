using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gibs.Domain.Entities
{
	public class FacSlip : AuditRecord
	{

		[Key]
		public long FacSlipID { get; set; }

		public string PolicyNo { get; set; }

		public string CertOrDocNo { get; set; }

		public decimal RiskValue { get; set; }

		public decimal ClassPremium { get; set; }

		public DateTime ReinStartDate { get; set; }

		public DateTime ReinEndDate { get; set; }

		public decimal AcceptRate { get; set; }

		public decimal AcceptValue { get; set; }

		public decimal QuotaRate { get; set; }

		public decimal QuotaValue { get; set; }

		public decimal TreatyRate { get; set; }

		public decimal TreatyValue { get; set; }

		public decimal FacBalanceRate { get; set; }

		public decimal RetentionRate { get; set; }

		public decimal RetentionValue { get; set; }

		public decimal AcceptPremium { get; set; }

		public string PartyID { get; set; }

		public Party Party { get; set; }

		public decimal ProportionOffer { get; set; }

		public decimal AmountCeded { get; set; }

		public decimal ReinCommission { get; set; }

		public decimal ReinPremium { get; set; }

		public int ReinDays { get; set; }

		public decimal ProRataPremium { get; set; }

		public byte Deleted { get; set; }

		public byte Active { get; set; }

		public string Tag { get; set; }

		public string Remarks { get; set; }

		public string Description { get; set; }

		public decimal RateOption { get; set; }

		public string Location { get; set; }

		public decimal Rate { get; set; }

		public DateTime ExtraDate { get; set; }

		public DateTime ExtraDate2 { get; set; }

	}
}
