using System.ComponentModel.DataAnnotations;

namespace Gibs.Domain.Entities
{
    public class BusinessBasics 
    {
        protected BusinessBasics() { /*EfCore*/ }

        public BusinessBasics(
            BusinessTypeEnum businessType,
            //BusinessSourceTypeEnum sourceType,
            Party party,
            DateOnly startDate,
            DateOnly endDate,
            int standardCoverDays,
            decimal ourShareRate,
            CommRate commission,
            decimal flatPremiumRate,
            FxCurrency? fxCurrency,
            FxCurrency localCurrency,
            decimal? fxRate)
        {
            fxCurrency ??= localCurrency;
            fxRate ??= 1;

            if (startDate >= endDate)
                throw new ArgumentOutOfRangeException(nameof(startDate),
                    $"{nameof(startDate)} cannot be later than {nameof(endDate)}");

            if (ourShareRate <= 0 || ourShareRate > 100)
                throw new ArgumentOutOfRangeException(nameof(ourShareRate),
                    $"{nameof(ourShareRate)} must be > 0 and <= 100");

            if (flatPremiumRate < 0 || flatPremiumRate > 100)
                throw new ArgumentOutOfRangeException(nameof(flatPremiumRate),
                    $"{nameof(flatPremiumRate)} must be between 0 - 100");

            if (commission.ComRate < 0 || commission.ComRate > 100)
                throw new ArgumentOutOfRangeException(nameof(commission.ComRate),
                    $"{nameof(commission.ComRate)} must be between 0 - 100");

            if (commission.VatRate < 0 || commission.VatRate > 100)
                throw new ArgumentOutOfRangeException(nameof(commission.VatRate),
                    $"{nameof(commission.VatRate)} must be between 0 - 100");

            if (standardCoverDays < 0)
                throw new ArgumentOutOfRangeException(nameof(standardCoverDays),
                    $"{nameof(standardCoverDays)} must be greater than or equal to ZERO");

            if (fxRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(fxRate),
                    $"{nameof(fxRate)} must be greater than ZERO");

            if (fxCurrency.Id != localCurrency.Id && fxRate == 1)
                throw new ArgumentOutOfRangeException(nameof(fxRate),
                    $"If {nameof(fxCurrency)} is NOT {localCurrency.Id}, the {nameof(fxRate)} cannot be 1");

            if (fxCurrency.Id == localCurrency.Id && fxRate != 1)
                throw new ArgumentOutOfRangeException(nameof(fxRate), 
                    $"If {nameof(fxCurrency)} is {localCurrency.Id}, the {nameof(fxRate)} must also be 1");

            TransDate = DateTime.UtcNow.Date;

            EndDate = endDate;
            StartDate = startDate;
            StandardCoverDays = standardCoverDays;
            OurShareRate = ourShareRate;
            FlatPremiumRate = flatPremiumRate;
            CommissionRate = commission.ComRate;
            CommissionVatRate = commission.VatRate;
            FxCurrencyId = fxCurrency.Id;
            FxRate = fxRate.Value;
            CoverDays = (endDate.DayNumber - startDate.DayNumber) + 1;

            BusinessType = businessType;
            SourceType = party.TypeId; // sourceType; 
            //ApprovalStatus = ApprovalStatusEnum.PENDING;

            switch (businessType)
            {
                case BusinessTypeEnum.DIRECT:
                case BusinessTypeEnum.CO_FOLLOW:
                //case BusinessTypeEnum.TREATY:
                    if (ourShareRate < 100)
                    {
                        throw new ArgumentOutOfRangeException(nameof(ourShareRate),
                            $"{BusinessType} must have [OurShareRate] = 100");
                    }
                    break;

                case BusinessTypeEnum.CO_LEAD:
                    //case BusinessTypeEnum.COINS_LEAD_SHARE_ONLY:
                    break;
            }
        }

        public DateTime TransDate { get; private set; }
        public string? CollectivePolicyNo { get; private set; }
        public BusinessTypeEnum? BusinessType { get; private set; }
        public string /*BusinessSourceTypeEnum*/ SourceType { get; protected set; }

        public DateOnly StartDate { get; private set; }
        public DateOnly EndDate { get; private set; }
        [Range(0, 365)]
        public int StandardCoverDays { get; private set; }
        public string FxCurrencyId { get; private set; }
        public decimal FxRate { get; private set; }
        [Range(0.001, 100)]
        public decimal OurShareRate { get; private set; } // Proportion Rate
        [Range(0.001, 100)]
        public decimal FlatPremiumRate { get; private set; }
        [Range(0.001, 100)]
        public decimal CommissionRate { get; protected set; }
        [Range(0.001, 100)]
        public decimal CommissionVatRate { get; protected set; }
        public int CoverDays { get; protected set; } // Prorata Days
        public byte AccountingType { get; private set; } = 100;
        
        public BusinessBasics ShallowCopy()
        {
            return (BusinessBasics)MemberwiseClone();
        }
    }
}
