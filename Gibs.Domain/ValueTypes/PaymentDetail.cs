namespace Gibs.Domain.Entities
{
    public class PaymentDetail
    {
        public PaymentDetail() { }

        public PaymentDetail(PaymentTypeEnum? paymentType, string? paymentReference, DateTime paymentDate, string? bankAccountNo, string? bankAccountName, string? ledgerAccount)
        {
            PaymentType = paymentType;
            PaymentReference = paymentReference;
            PaymentDate = paymentDate;
            BankAccountNo = bankAccountNo;
            BankAccountName = bankAccountName;
            LedgerAccount = ledgerAccount;
        }

        public PaymentTypeEnum? PaymentType { get; private set; }  
        public string? PaymentReference { get; private set; }  
        public DateTime PaymentDate { get; private set; }  
        public string? BankAccountNo { get; private set; }  
        public string? BankAccountName { get; private set; }  
        public string? LedgerAccount { get; private set; }  

        public PaymentDetail ShallowCopy()
        {
            return (PaymentDetail)MemberwiseClone();
        }
    }
}
