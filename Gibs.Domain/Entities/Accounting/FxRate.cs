
namespace Gibs.Domain.Entities
{
    public class FxRate : AuditRecord
    {
        public long Id { get; private set; }
        public string CurrencyId { get; private set; }
        public decimal RateValue { get; private set; }

        //Navigation Properties
        public virtual FxCurrency Currency { get; private set; }

        protected FxRate() { /*EfCore*/ }

        internal FxRate(FxCurrency currency, decimal rateValue)
        {
            CurrencyId = currency.Id;
            Currency = currency;
            RateValue = rateValue;
        }
    }
}

