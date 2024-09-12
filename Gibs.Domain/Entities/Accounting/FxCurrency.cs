
namespace Gibs.Domain.Entities
{
	public class FxCurrency
	{
		public string Id { get; private set; }
		public string Name { get; private set; }
        public string Symbol { get; private set; }
		public decimal CurrentRate { get; private set; }

		public bool Active { get; private set; }

		//Navigation Properties
        public virtual ReadOnlyList<FxRate> Rates { get; private set; }

        protected FxCurrency() { /*EfCore*/ }

        public void SetNewRate(decimal newRate)
        {
            CurrentRate = newRate;

            FxRate rate = new(this, newRate);
            ((ICollection<FxRate>)Rates).Add(rate);
        }

        public static FxCurrency FactoryCreate(string Id, string name, string symbol, decimal currentRate)
        {
            //validation

            return new FxCurrency()
            {
                Id = Id,
                Name = name,
                Active = true,
                Symbol = symbol,
                CurrentRate = currentRate,
            };
        }
    }
}

