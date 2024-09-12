
namespace Gibs.Domain.Entities
{
    public class CommRate : AuditRecord
    {
        public string RiskOptionId { get; private set; } //either ProductID, MidRiskID, RiskID
        public string? PartyOptionId { get; private set; } = "";  //either EMPTY=Any, PartnerKindID(eg AGENT,BROKER, ), PartnerID
        public decimal ComRate { get; private set; }
        public decimal VatRate { get; private set; }

        public string? Remarks { get; private set; }

        protected CommRate() { /*EfCore*/ }
    }
}