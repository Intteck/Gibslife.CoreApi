using System.Linq;
using System.Collections.Generic;
using Gibs.Domain.Entities;

namespace Gibs.Api.Contracts.V1
{
    public class PolicySectionContract
    {
        internal static PolicySectionContract Create(PolicySection section)
        {
            return new PolicySectionContract
            {
                SectionID = section.SectionId,
                SectionPremium = section.ItemPremium,
                SectionSumInsured = section.ItemSumInsured,

                Fields = section.GetFieldRecords().Select(FieldContract.Create),
                Rates = section.GetRateRecords().Select(RateContract.Create),
                SMIs = section.GetSMIRecords().Select(SmiContract.Create),
            };
        }

        public required string SectionID { get; init; }
        public required decimal SectionSumInsured { get; init; }
        public required decimal SectionPremium { get; init; }

        public required IEnumerable<FieldContract> Fields { get; init; } = [];
        public IEnumerable<RateContract>? Rates { get; init; } 
        public IEnumerable<SmiContract>? SMIs { get; init; }      

        internal SectionRecord AsRecord()
        {
            List<FieldRecord> fields = [];
            List<RateRecord> rates = [];
            List<SmiRecord> smis = [];

            if (Rates != null)
                rates = Rates.Select(x => x.AsRecord()).ToList();

            if (Fields != null)
                fields = Fields.Select(x => x.AsRecord()).ToList();

            if (SMIs != null)
                smis = SMIs.Select(x => x.AsRecord()).ToList();

            return new SectionRecord(
                SectionID, SectionSumInsured, SectionPremium, fields, rates, smis);
        }
    }

    public class QuoteSectionContract
    {
        public required string SectionID { get; init; }
        public IEnumerable<RateContract>? Rates { get; init; }
        public IEnumerable<SmiContract>? SMIs { get; init; }      
    }

    public class FieldContract
    {
        internal static FieldContract Create(FieldRecord field)
        {
            return new FieldContract
            {
                Code = field.Code,
                Value = field.Value,
            };
        }

        public required string Code { get; init; }
        public required string Value { get; init; }

        internal FieldRecord AsRecord()
        {
            return new FieldRecord(Code, Value);
        }
    }

    public class RateContract
    {
        internal static RateContract Create(RateRecord rate)
        {
            return new RateContract
            {
                Code = rate.Code,
                Value = rate.Value,
            };
        }

        public required string Code { get; init; }
        public required string Value { get; init; }

        internal RateRecord AsRecord()
        {
            return new RateRecord(Code, Value);
        }
    }

    public class SmiContract
    {
        internal static SmiContract Create(SmiRecord smi)
        {
            return new SmiContract
            {
                Code = smi.SmiId,
                Premium = smi.Premium,
                SumInsured = smi.SumInsured,
                PremiumRate = smi.PremiumRate,
                Description = smi.Description,
            };
        }

        public required string Code { get; init; }
        public required decimal SumInsured { get; init; }
        public required decimal Premium { get; init; }
        public required decimal PremiumRate { get; init; }
        public required string Description { get; init; } = "Sample SMI description goes here";

        internal SmiRecord AsRecord()
        {
            return new SmiRecord(Code, SumInsured, Premium, PremiumRate, Description);
        }
    }
}
