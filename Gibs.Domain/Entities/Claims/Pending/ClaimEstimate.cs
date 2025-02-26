namespace Gibs.Domain.Entities
{
	public class ClaimEstimate : AuditRecord
	{
		public long ReserveID { get; set; }

		public string BranchID { get; set; }

		public DateTime EntryDate { get; set; }

		public DateTime NotifyDate { get; set; }

		public DateTime LossDate { get; set; }

		public string ClaimNo { get; set; }

		public string PolicyNo { get; set; }

		public string CoPolicyNo { get; set; }

		public string RefDNCNNo { get; set; }

		public decimal ProportionRate { get; set; }

		public int UndYear { get; set; }

		public string InsuredName { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public string ProductID { get; set; }
		public Product Product { get; set; }

		public string PartyID { get; set; }
		public Party Party { get; set; }

		public string InsuredID { get; set; }

		public string LossType { get; set; }

		public string LossDetails { get; set; }

		public DateTime ExtraDate { get; set; }

		public DateTime ExtraDate2 { get; set; }

		public string Status { get; set; }

		public decimal AmountReserved { get; set; }

		public decimal AmountPaid { get; set; }

		public long DetailID { get; set; }

		public byte Approval { get; set; }

		public byte Deleted { get; set; }

		public byte Active { get; set; }

		public string ApprovedBy { get; set; }

		public DateTime ApprovedOn { get; set; }

		public string LeadID { get; set; }

		public string Leader { get; set; }

		public string ProportionType { get; set; }

		public decimal LeadProportion { get; set; }
	}
}
