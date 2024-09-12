using System.ComponentModel.DataAnnotations;

namespace Gibs.Domain.Entities
{
    public class ProductCover
	{
		[Key]
		public string CoverID { get; private set; }

		public string ProductID { get; private set; }

		public bool AddToSI { get; private set; }

		public bool AddToRSI { get; private set; }

		public decimal DefaultValue { get; private set; }

		public string DefaultValueType { get; private set; }

		//Navigation Properties
	}

	public class ProductSMICover
	{
		[Key]
		public int SmiID { get; private set; }
		[Key]
		public string CoverID { get; private set; }

		//Navigation Properties
	}

}
