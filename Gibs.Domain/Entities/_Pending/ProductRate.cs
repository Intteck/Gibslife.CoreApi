using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gibs.Domain.Entities
{
	public class ProductRate
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ProductRateID { get; set; }

		public string Description { get; set; }

		public string ProductID { get; set; }

		public decimal Rate { get; set; }

		//Public Property Active As Boolean

		//Public Property Deleted As Boolean

		//Public Property SubmittedBy As String

		//Public Property SubmittedOn As Date

		//Public Property Modifiedby As String

		//Public Property ModifiedOn As Date

	}
}
