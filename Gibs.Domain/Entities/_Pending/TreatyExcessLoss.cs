using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gibs.Domain.Entities
{
	public class TreatyExcessLoss : AuditRecord
	{

		[Key]
		public long TreatyExcessLossID { get; set; }

		public string ProductID { get; set; }

		public string PartyID { get; set; }

		public string UnderYear { get; set; }

		public string CoverID { get; set; }
		public ProductCover Cover { get; set; }

		public decimal Deductible { get; set; }

		public decimal MDPremium { get; set; }

		public decimal Cover2 { get; set; }

		public decimal Deductible2 { get; set; }

		public decimal MDPremium2 { get; set; }

		public string ReinstateNo { get; set; }

		public decimal ReinstatePercent { get; set; }

		public decimal AdjustRate { get; set; }

		public decimal AdjustRate2 { get; set; }

		public decimal Adjust2ndRate { get; set; }

		public decimal Adjust2ndRate1 { get; set; }

		public string LoadingFactor { get; set; }

		public string Tag { get; set; }

	}
}
