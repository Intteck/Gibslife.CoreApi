using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gibs.Domain.Entities
{
	public class ClassRange : AuditRecord
	{
		[Key]
		public long ClassRangeID { get; set; }

		public string ProductID { get; set; }

		public string UnderYear { get; set; }

		public decimal Range { get; set; }

		public decimal Range2 { get; set; }

		public long Tag { get; set; }

	}
}
