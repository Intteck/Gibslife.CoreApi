//using System.ComponentModel.DataAnnotations;

//namespace Gibs.Domain.Entities
//{
//    public class Receipt : NoteBase
//    {
//        [Required]
//        public string ReceiptNo { get; protected set; }

//        public decimal AmountPaid { get; protected set; }
//        public decimal AmountPaidFx { get; protected set; }
//        public decimal BatchAmountPaid { get; protected set; }


//        public ReceiptGrossNetEnum? GrossOrNetOption { get; protected set; }
//        public RecieptPrintNameEnum? PrintNameOption { get; protected set; }

//        public string? DepositorName { get; protected set; }
//        public string? SecurityCode { get; protected set; }
//        public string? CustomPrintName { get; protected set; }

//        public ProcessCommissionEnum AccountProcessing { get; protected set; } = ProcessCommissionEnum.PROCESS;

//        //RecieptNo may be duplicated for multiple debit notes, but transguid will be same
//        //does this mean the PK should be [TransGUID, RecieptNo] ?


//        public Receipt() { }

//        public Receipt(ICodeNumberFactory codeFactory, DebitNote dn, PaymentDetail payment, Party party) : base(dn)
//        {
//            ReceiptNo = codeFactory.CreateCodeNumber(CodeTypeEnum.RECEIPT, null);
//            Narration = $"Being reciept for Debit Note No: {dn.DebitNoteNo}";

//            //PolicyNo = dn.PolicyNo;
//            RefNoteNo = dn.DebitNoteNo;
//            GrossOrNetOption = ReceiptGrossNetEnum.NET;
//            PrintNameOption = RecieptPrintNameEnum.SHOW_CUSTOMER;

//            if (party.CommissionModeID == CommissionModeEnum.INCLUDED)
//            {
//                AmountPaid = dn.Premium.GrossPremium;
//                AccountProcessing = ProcessCommissionEnum.PROCESS;
//                GrossOrNetOption = ReceiptGrossNetEnum.GROSS;
//            }

//            if (party.CommissionModeID == CommissionModeEnum.DEDUCTED)
//            {
//                AmountPaid = dn.Premium.NetProratedPremium;
//                AccountProcessing = ProcessCommissionEnum.DONT_PROCESS;
//            }

//            if (party.CommissionModeID == CommissionModeEnum.NONE)
//            {
//                AmountPaid = dn.Premium.NetProratedPremium;
//                AccountProcessing = ProcessCommissionEnum.DONT_PROCESS;
//            }

//            if (dn.Business.CommissionRate == 0)
//                AccountProcessing = ProcessCommissionEnum.DONT_PROCESS;


//            BatchAmountPaid = AmountPaid; //How do we handle batch payments? 
//            AmountPaidFx = AmountPaid / dn.Business.FxRate;

//            ExFields = new DbExtendedFields
//            {
//                AmountPaid = AmountPaid,
//                AmountPaidFx = AmountPaidFx,
//            };

//            //Payment = payment.ShallowCopy();
//            //dn.UpdatePayment(payment);
//        }

//        public void Approve(User approver)
//        {
//            Secure.Approve(approver);    
//        }



//        public DbExtendedFields ExFields { get; }

//        public record class DbExtendedFields
//        {
//            public decimal AmountPaid { get; init; }
//            public decimal AmountPaidFx { get; init; }
//        }
//    }

//    public enum ProcessCommissionEnum
//    {
//        PROCESS = 0,
//        DONT_PROCESS = 2,
//    }

//    public enum RecieptPrintNameEnum
//    {
//        [GibsValue("DIRECT")]
//        SHOW_CUSTOMER,
//        [GibsValue("BROKERS")]
//        SHOW_BROKERS,
//        [GibsValue("OTHERS")]
//        CUSTOMIZE
//    }

//    public enum ReceiptGrossNetEnum
//    {
//        GROSS, 
//        NET,  
//    }

//}

