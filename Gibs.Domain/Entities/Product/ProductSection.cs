namespace Gibs.Domain.Entities
{
    public class ProductSection
	{
		public string ProductId { get; private set; }
		public string SectionId { get; private set; }
		public string SectionName { get; private set; }

		//Navigation Properties
		public ReadOnlyList<ProductSMI> SMIs { get; init; }
	}

	public class ProductSMI
	{
		public string ProductId { get; private set; }
		public string SectionId { get; private set; }
		public string SmiId { get; private set; }
		public string SmiName { get; private set; }

		//Navigation Properties
		public ProductSection Section { get; private set; }
	}

}
