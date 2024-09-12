namespace Gibs.Domain.Entities
{
	public class ClaimPayment : AuditRecord
	{
		public long PaymentID { get; set; }

		public string BranchID { get; set; }

		public DateTime ProcessDate { get; set; }

		public string ClaimNo { get; set; }

		public string PolicyNo { get; set; }

		public string PayType { get; set; }

		public string PayMode { get; set; }

		public string ClaimType { get; set; }

		public string RefDNCNNo { get; set; }

		public string Payee { get; set; }

		public string PayeeType { get; set; }

		public string InsuredName { get; set; }

		public string ProductID { get; set; }
		public Product Product { get; set; }

		public string PartyID { get; set; }
		public Party Party { get; set; }

		public decimal InsuredFee { get; set; }

		public decimal FacRecovery { get; set; }

		public decimal TreatyRecovery { get; set; }

		public decimal OtherExpenses { get; set; }

		//Public Property A1 As Decimal
		//Public Property A2 As Decimal
		//Public Property A3 As Decimal


		private decimal LessSalvage;

		private decimal CoinDeduction;

		private decimal AdjustedAmount;

		private decimal TotalAmount;

		private string Description;
		//Public Property Field1 As String
		//Public Property Field2 As String
		//Public Property Field3 As String



		public string AdjusterCode { get; set; }

		public string AdjusterName { get; set; }

		public string ChequeNo { get; set; }

		public DateTime PaymentDate { get; set; }

		public DateTime DischargeDate { get; set; }

		public DateTime DischargeDate2 { get; set; }

		public DateTime DischargeDate3 { get; set; }

		public DateTime ExtraDate { get; set; }

		public DateTime ExtraDate2 { get; set; }

		public string Remarks { get; set; }

		public decimal PropPercent { get; set; }

		//Public Property SubmittedBy As String
		//Public Property SubmittedOn As Date
		//Public Property ModifiedBy As String
		//Public Property ModifiedOn As Date

		public string ApprovedBy { get; set; }

		public DateTime ApprovedOn { get; set; }

		public string ConfirmedBy { get; set; }

		public DateTime ConfirmedOn { get; set; }

		public string BankAccountCode { get; set; }

		public string ClaimsAccountCode { get; set; }

		public string DeletedBy { get; set; }

		public DateTime DeletedOn { get; set; }

		public string Status { get; set; }

		public byte Deleted { get; set; }

		public byte Active { get; set; }
	}
}
