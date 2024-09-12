using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gibs.Domain.Entities
{
	public class TreatyProportion : AuditRecord
	{

		[Key]
		public long TreatyPropID { get; set; }

		public string ProductID { get; set; }

		public string PartyID { get; set; }

		public string UnderYear { get; set; }

		public decimal PremiumRate { get; set; }

		public decimal CommissionRate { get; set; }

		public decimal ProfitComm { get; set; }

		public decimal MgtExpenseRate { get; set; }

		public decimal LossLimitRate { get; set; }

		public decimal PortfolioPrem { get; set; }

		public decimal PortfolioLoss { get; set; }

		public string Tag { get; set; }

	}
}
