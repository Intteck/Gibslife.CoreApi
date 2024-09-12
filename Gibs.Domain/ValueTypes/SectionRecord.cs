
namespace Gibs.Domain.Entities
{
    public record class SectionRecord(
        string SectionId,
        decimal SumInsured,
        decimal Premium,
        IEnumerable<FieldRecord> Fields,
        IEnumerable<RateRecord> Rates,
        IEnumerable<SmiRecord>? Smis)
    {
        public static SectionRecord CopyFrom(PolicySection s)
        {
            return new SectionRecord(
                s.SectionId,
                s.ItemSumInsured,
                s.ItemPremium,
                s.GetFieldRecords(true),
                s.GetRateRecords(),
                s.GetSMIRecords()
            );
        }
    };

    public record class SmiRecord(string SmiId, decimal SumInsured, decimal Premium, decimal PremiumRate, string Description);

    public record class RateRecord(string Code, string Value);

    public record class FieldRecord(string Code, string Value);
}
