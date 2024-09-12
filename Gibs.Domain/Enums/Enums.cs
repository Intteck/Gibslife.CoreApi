
namespace Gibs.Domain.Entities
{
    public enum CustomerTypeEnum
    {
        INDIVIDUAL,
        CORPORATE,
        UNKNOWN,
    }

    public enum GenderEnum
    {
        [GibsValue("MALE", "SELECT", "")]
        MALE,
        FEMALE,
        UNKNOWN
    }

    public enum KycTypeEnum
    {
        //--INTL. PASSPORT	6534-
        //--NATIONAL ID CARD	7250-
        //--OTHERS			327-
        //--NONE            226
        //--RC NO.			235
        //--[EMPTY STRING]  116941
        //--DRIVER LICENSE	1508-
        [GibsValue("OTHERS", "NONE", "")]
        NOT_AVAILABLE,
        [GibsValue("DRIVER LICENSE", "DRIVERS LICENSE")]
        DRIVERS_LICENSE,
        [GibsValue("INTL. PASSPORT")]
        PASSPORT,
        [GibsValue("NATIONAL ID CARD")]
        NATIONAL_ID_CARD,
        [GibsValue("RC NO.")]
        COMPANY_REG_NO
    }

    public enum ApprovalEnum : byte 
    {
        PENDING = 0,
        APPROVED = 1,
        APPROVED2 = 2
    }

    public enum CommTypeEnum
    {
        INCLUDED = 0,   //NORMAL. COMMISSION IS INCLUDED WITH PREMIUM PAID
        DEDUCTED = 1,   //DEDUCTED FROM SOURCE
        NONE     = 2,   //NO COMMISSION
    }

    public enum EndorsementTypeEnum         //aka BizOption 
    {
        //5 Positives
        NEW,
        RENEWAL,
        ADDITIONAL,
        EXTENSION,
        REDUCTION,

        //5 Negatives
        RETURN,
        REVERSAL,
        [GibsValue("CANCEL")]
        CANCELLED,
        EXPIRED, //??
        NIL,
    }

    public enum BusinessTypeEnum            //aka BizSource
    {
        [GibsValue("DIRECT")]
        DIRECT,
        [GibsValue("CO-INSURANCE LEAD")]
        CO_LEAD,
        [GibsValue("ACCEPTED")]
        CO_FOLLOW,
        [GibsValue("FAC-IN")]
        FAC_IN,
        //[GibsValue("SURPLUS TREATY")]
        //TREATY,
    }

    public enum PaymentTypeEnum  
    {
        [GibsValue("CREDIT ADVISE")]
        CREDIT_ADVISE,
        [GibsValue("CREDIT NOTE")]
        CREDIT_NOTE,
        [GibsValue("DEBIT NOTE")]
        DEBIT_NOTE,

        [GibsValue("MOBILE MONEY")]
        MOBILE_MONEY,
        [GibsValue("TRANSFER")]
        TRANSFER,

        [GibsValue("DIRECT CASH")]
        DIRECT_CASH,
        [GibsValue("DIRECT CHEQUE")]
        DIRECT_CHEQUE,

        [GibsValue("NORMAL")]
        NORMAL,
        [GibsValue("ORC")]
        ORC,



        [GibsValue("CASH")]
        CASH,
        [GibsValue("CHEQUE")]
        CHEQUE,
        [GibsValue("POS")]
        POS,
        [GibsValue("COMMISSION_EXP")]
        COMMISSION_EXP,
        [GibsValue("COM_BATCH_PROCESS")]
        COM_BATCH_PROCESS,
        //[GibsValue("PD CHEQUE")]
        //PD_CHEQUE,
    }
}

//[BizOption]   =    Commission, GROSS, NEW, ORC Charges, RENEWAL

//[SourceType]  =    NULL, AG, AGENTS, BR, BROKERS, DC, DI, DIRECT, DIRECT_COM, EC

//[NoteType]    =    CN, CNJ, DN, DNJ, PYM, RCP

//[PaymentType] =    CASH, CHEQUE, TRANSFER, - reciept
//                   CREDIT NOTE, 
//                   DIRECT CASH, DIRECT CHEQUE, - dn
//                   NORMAL, NULL, ORC,  - 
