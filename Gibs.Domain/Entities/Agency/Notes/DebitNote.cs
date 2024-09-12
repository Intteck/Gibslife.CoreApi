//using System.ComponentModel.DataAnnotations;

namespace Gibs.Domain.Entities
{
    public class DebitNote : NoteBase
    {
        //[Required]
        public string DebitNoteNo { get; protected set; }
        public string? DeclarationNo { get; private set; }
        //[Required]
        //public BusinessPremium Premium { get; protected set; }

        //public DebitNoteTypeEnum? DebitNoteType { get; protected set; }  
        //public EndorsementTypeEnum? Endorsement { get; protected set; }  
        //public BusinessTypeEnum? BusinessType { get; protected set; }

        //public BusinessSourceTypeEnum? BusinessSource { get; protected set; }

        // Navigation
        //public virtual Policy Policy { get; protected set; }
        public virtual PolicyHistory History { get; private set; }

        public DebitNote() { }

        public DebitNote(ICodeNumberFactory codeFactory, PolicyHistory history/*, DebitNoteTypeEnum debitNoteType*/) : base(history)
        {
            History = history;
            //Premium = history.Premium.ShallowCopy();

            DeclarationNo = history.DeclareNo;
            DebitNoteNo = codeFactory.CreateCodeNumber(CodeTypeEnum.DNOTE, null);
            Narration = "Being policy premium for Policy No. " + history.Policy.PolicyNo;

            //DebitNoteType = debitNoteType;
            //Endorsement = history.Endorsement;
            //BusinessType = Business.BusinessType;
            //BusinessSource = Business.SourceType;
            Naicom = NaicomRecord.FactoryCreate();
        }

        //public void UpdatePayment(PaymentDetail payment)
        //{
        //    Payment = payment.ShallowCopy();
        //}

        //public Receipt CreateReceipt(ICodeNumberFactory codeFactory, PaymentDetail payment, Party party)
        //{
        //    return new Receipt(codeFactory, this, payment, party);
        //}

        public void Approve(User approver)
        {
            Approve(approver);                 //debitNote
            History.Approve(approver);         //policyHistory
        }

        public void UpdateNaicom(NaicomRecord naicom)
        {
            ArgumentNullException.ThrowIfNull(naicom);

            History.UpdateNaicom(naicom);
            Naicom = naicom;
        }
    }

    //public enum DebitNoteTypeEnum
    //{
    //    // saved into REMARKS field
    //    NORMAL, INVOICE, ORC
    //}
}

