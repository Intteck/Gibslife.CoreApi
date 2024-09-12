
namespace Gibs.Domain.Entities
{
    public class BusinessPremium
    {
        protected BusinessPremium() { }

        internal BusinessPremium(
            BusinessBasics business,
            PolicyHistory history)
        {
            BaseSumInsuredFx = history.Sections.Sum(x => x.ItemSumInsured);
            BasePremiumFx = history.Sections.Sum(x => x.ItemPremium);

            //convert to Local currency for saving
            BaseSumInsured = BaseSumInsuredFx * business.FxRate;
            BasePremium = BasePremiumFx * business.FxRate;

            decimal discounts = 0, loadings = 0; //for future use
            //apply loading and discounts
            SumInsuredFx = BaseSumInsuredFx - discounts + loadings;
            GrossPremiumFx = BasePremiumFx - discounts + loadings;

            //convert to Local currency for saving
            SumInsured = SumInsuredFx * business.FxRate;
            GrossPremium = GrossPremiumFx * business.FxRate;

            if (business.CommissionRate > 0)
                Commission = GrossPremium * business.CommissionRate / 100;

            if (business.CommissionVatRate > 0)
                CommissionVat = Commission * business.CommissionVatRate / 100;

            if (business.FlatPremiumRate > 0 && GrossPremium == 0)
                GrossPremium = SumInsured * business.FlatPremiumRate / 100;

            if (business.BusinessType == BusinessTypeEnum.DIRECT)
            {
                // OurShare = 100
                ShareSumInsured = SumInsured;
                SharePremium = GrossPremium;
            }
            else if (business.BusinessType == BusinessTypeEnum.CO_LEAD)
            {
                //TODO: check TrackID for 100% accounting OR OurShareAccounting
                ShareSumInsured = SumInsured * business.OurShareRate / 100;
                SharePremium = GrossPremium * business.OurShareRate / 100;
            }
            else if (business.BusinessType == BusinessTypeEnum.CO_FOLLOW)
            {
                ShareSumInsured = SumInsured;
                SharePremium = GrossPremium;
                //OurShareSumInsured = SumInsured * business.OurShareRate / 100;
                //OurSharePremium = GrossPremium * business.OurShareRate / 100;
            }

            ProrataPremium = SharePremium;

            if (business.StandardCoverDays > 0)
                ProrataPremium = SharePremium * business.CoverDays / business.StandardCoverDays;
                //ProratedPremium = OurSharePremium * business.CoverDays / 365;

            NetProrataPremium = ProrataPremium - Commission; 

            WholeSumInsured = SumInsured * 100 / business.OurShareRate;
            WholePremium = GrossPremium * 100 / business.OurShareRate;
        }

        public BusinessPremium ShallowCopy()
        {
            return (BusinessPremium)MemberwiseClone();
        }

        public decimal BasePremiumFx { get; protected set; }
        public decimal BasePremium { get; protected set; }
        public decimal GrossPremiumFx { get; protected set; }
        public decimal GrossPremium { get; protected set; }
        public decimal WholePremium { get; protected set; }
        public decimal SharePremium { get; protected set; }
        public decimal ProrataPremium { get; protected set; }
        public decimal NetProrataPremium { get; protected set; }

        public decimal Commission { get; protected set; }
        public decimal CommissionVat { get; protected set; }

        public decimal BaseSumInsuredFx { get; protected set; }
        public decimal BaseSumInsured { get; protected set; }
        public decimal SumInsuredFx { get; protected set; }
        public decimal SumInsured { get; protected set; }
        public decimal ShareSumInsured { get; protected set; }
        public decimal WholeSumInsured { get; protected set; }
    }
}
