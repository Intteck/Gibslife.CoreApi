using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gibs.Domain.Entities
{
	public enum CodeTypeEnum : byte
	{
		AGENCY,
		AGENTS,
		ASSET_CODE,
		BROKER,
		CERTNO,
		CESSION_NO,
		CESSIONNO,
		CLAIM,
		CLAIM_NOTIFY,
		CNOTE,
		DECLARATION_NO,
		DIRECT,
		DNOTE,
		ENDTNO,
		INSCO,
		INSURED,
		INVOICE,
		JOURNAL,
		MKTSTAFF,
		OCRCNOTE,
		ORCCNOTE,
		ORCDNOTE,
		OTHERS,
		POLICY,
		PROPOSAL,
		RECEIPT,
		RECEIPT_UNALLOCATED,
		REINCO,
		VOUCHER
	}
	[Flags]
	public enum SerialIncrementFieldEnum : byte
	{
		[Description("Global")]
		GLOBAL = 0,
		[Description("Branch Only")]
		BRANCH = 1,
		[Description("Class Only")]
		CLASS = 2,
		[Description("Mid-Class Only")]
		MIDCLASS = 4,
		[Description("Product Only")]
		PRODUCT = 8,
		[Description("Branch & Class")]
		BRANCH_CLASS = BRANCH | CLASS,
		[Description("Branch & Mid-Class")]
		BRANCH_MIDCLASS = BRANCH | MIDCLASS,
		[Description("Branch & Product")]
		BRANCH_PRODUCT = BRANCH | PRODUCT
	}

	public enum SerialResetEnum : byte
	{
		[Description("Never")]
		NEVER = 0,
		[Description("Every Year")]
		YEARLY = 1,
		[Description("Every Month")]
		MONTHLY = 2
		//<Description("Every Week")>
		//    WEEKLY = 4
		//<Description("Every Day")>
		//    DAILY = 8
	}

	public class CodeNumber
	{
		[Key, StringLength(20, MinimumLength = 4)]
		public string CodeNumberID { get; private set; }            //CodeTypeEnum
		[Required]
		public string SerialIncrementField { get; private set; }    //SerialIncrementFieldEnum
		[Required]
		public string SerialResetMode { get; private set; }         //SerialResetEnum

		public string Format { get; private set; }
		[Required]
		public long NextValue { get; private set; }

		public DateTime? LastUpdatedOn { get; private set; }

		public DateTime? LastResetOn { get; private set; }

		public string Sample { get; private set; }

		//Navigation Properties
		public virtual ReadOnlyList<CodeNumberDetail> CodeNumberDetails { get; private set; }

		protected CodeNumber() { /*EfCore*/ }

		public void Update(SerialIncrementFieldEnum SerialIncrementField,
			SerialResetEnum SerialResetMode, string Format, string Sample)
		{
			this.SerialIncrementField = SerialIncrementField.ToString();
			this.SerialResetMode = SerialResetMode.ToString();
			this.Format = Format;
			this.Sample = Sample;
		}

		public string GetNextCodeNumber()
		{
			NextValue += 1;
			LastUpdatedOn = DateTime.UtcNow;
			return string.Format("{0}", NextValue);
		}

		public void ManualReset()
		{
			NextValue = 0;
			LastResetOn = DateTime.UtcNow;
			LastUpdatedOn = DateTime.UtcNow;
		}

	}

	public class CodeNumberDetail
	{
		[Key, Column(Order = 0), StringLength(20, MinimumLength = 4)]
		public string CodeNumberID { get; private set; }
		[Key, Column(Order = 1)]
		public string SerialIncrementFieldID { get; private set; }

		public string ClassID { get; private set; }

		public string MidClassID { get; private set; }

		public string ProductID { get; private set; }

		public string Format { get; private set; }
		[Required]
		public long NextValue { get; private set; }

		public DateTime? LastUpdatedOn { get; private set; }

		public DateTime? LastResetOn { get; private set; }

		public string Sample { get; private set; }
		//Navigation Properties
		public virtual CodeNumber CodeNumber { get; private set; }

		protected CodeNumberDetail() { /*EfCore*/ }

	}
}
