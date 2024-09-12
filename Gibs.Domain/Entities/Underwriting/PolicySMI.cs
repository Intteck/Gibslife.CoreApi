
namespace Gibs.Domain.Entities
{
	public class PolicySMI : AuditRecord
	{
		#pragma warning disable CS8618
		private PolicySMI() /*EfCore*/ { }
		#pragma warning restore CS8618

		public PolicySMI(ProductSMI productSMI, PolicySection section, SmiRecord request)
		{
			SmiId = productSMI.SmiId;
			SmiName = productSMI.SmiName;
			SectionId = productSMI.Section.SectionId;
			ProductId = section.ProductId;
			//SectionName = productSMI.Section.SectionName;

			//PolicyNo = section.PolicyNo;
			//CertificateNo = section.CertificateNo;
			DeclareNo = section.DeclareNo;
			Description = request.Description;

			PremiumRate = request.PremiumRate;
			//tag =	section.History.Endorsement
			
			TotalSumInsured = request.SumInsured;
			TotalPremium = request.Premium;			
			ShareSumInsured = TotalSumInsured * PremiumRate /100;
			SharePremium = TotalPremium * PremiumRate / 100;

			//UpdateCreatedBy("E-CHANNEL");
		}

        public long SerialNo { get; }
        public string ProductId { get; private set; }
        public string SectionId { get; private set; }
		public string SmiId { get; private set; }
		public string SmiName { get; private set; }
		public string DeclareNo { get; private set; }

		//public string? CertificateNo { get; private set; }
		public string Description { get; private set; }

		public decimal PremiumRate { get; private set; }
		public decimal TotalSumInsured { get; private set; }
		public decimal TotalPremium { get; private set; }
		public decimal ShareSumInsured { get; private set; }
		public decimal SharePremium { get; private set; }


		//Navigation Properties
		//public virtual PolicySection Section { get; protected set; }
	}
}
