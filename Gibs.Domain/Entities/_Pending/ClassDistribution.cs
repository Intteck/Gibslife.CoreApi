using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gibs.Domain.Entities
{
	public class ClassDistribution : AuditRecord
	{
		[Key]
		public long DistributionID { get; set; }

		public string ProductID { get; set; }

		public string UnderYear { get; set; }

		public decimal CessionRate { get; set; }

		public decimal Retention { get; set; }

		public decimal Surplus { get; set; }

		public decimal Surplus2 { get; set; }

		public decimal LineNumbers { get; set; }

		public decimal TreatyCapacity { get; set; }

		public string TransGuid { get; set; }

		public string Tag { get; set; }

	}
}
