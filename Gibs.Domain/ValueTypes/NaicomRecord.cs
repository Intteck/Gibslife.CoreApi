using System.ComponentModel.DataAnnotations.Schema;

namespace Gibs.Domain.Entities
{
    public class NaicomRecord
    {
        public enum NaicomStatusEnum
        {
            [GibsValue("QUEUED")]
            PENDING, 
            [GibsValue("SENT")]
            SUCCESS,   
            [GibsValue("IGNORED")]
            FAIL,
            [GibsValue("ARCHIVED")]
            UNUSED,
        }

        [Column("Z_NAICOM_UID", TypeName = "varchar(200)"/*,   Order = 995*/)] public string? UniqueId { get; private set; }
        [Column("Z_NAICOM_SENT_ON", TypeName = "datetime2"/*,  Order = 996*/)] public DateTime? SubmitDate { get; private set; }
        [Column("Z_NAICOM_STATUS", TypeName = "varchar(20)"/*, Order = 997*/)] public NaicomStatusEnum Status { get; private set; } = NaicomStatusEnum.PENDING;
        [Column("Z_NAICOM_ERROR", TypeName = "varchar(max)"/*, Order = 998*/)] public string? ErrorMessage { get; private set; }
        [Column("Z_NAICOM_JSON", TypeName = "varchar(max)"/*,  Order = 999*/)] public string? JsonPayload { get; private set; }

        protected NaicomRecord() { /*EfCore*/ }

        public static NaicomRecord Success(string uniqueId)
        {
            return new NaicomRecord
            {
                UniqueId = uniqueId,
                Status = NaicomStatusEnum.SUCCESS,
                SubmitDate = DateTime.Now
            };
        }

        public static NaicomRecord FactoryCreate()
        {
            return new NaicomRecord
            {
                Status = NaicomStatusEnum.PENDING,
            };
        }

        public static NaicomRecord FailedPermanent(string errorMessage)
        {
            return new NaicomRecord
            {
                Status = NaicomStatusEnum.FAIL,
                ErrorMessage = errorMessage,
                //JsonPayload = jsonPayload,
                SubmitDate = DateTime.Now
            };
        }

        public static NaicomRecord FailedPermanent(IList<string> errorMessages, string jsonPayload)
        {
            var errorMessage = string.Join(",", errorMessages);
            return FailedPermanent(errorMessage, jsonPayload);
        }

        public static NaicomRecord FailedPermanent(string errorMessage, string jsonPayload)
        {
            return new NaicomRecord
            {
                Status = NaicomStatusEnum.FAIL,
                ErrorMessage = errorMessage,
                JsonPayload = jsonPayload,
                SubmitDate = DateTime.Now
            };
        }
    }
}
