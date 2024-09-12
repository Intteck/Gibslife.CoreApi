namespace Gibs.Domain.Entities
{
	public abstract class NoteBase : ApprovalRecord
    {
        protected NoteBase() { }

        protected NoteBase(PolicyHistory history)
        {
            PolicyNo = history.Policy.PolicyNo;
            BranchId = history.Policy.Members.BranchId;
            PartyId = history.Policy.Members.PartyId;
            PartyName = history.Policy.Members.PartyName;

            Amount = history.Premium.GrossPremium;
            CurrencyId = history.Business.FxCurrencyId;
            FxRate = history.Business.FxRate;
        }

        public string BranchId { get; private set; }
        public string PolicyNo { get; private set; }
		public string? Narration { get; protected set; }

        public string PartyId { get; private set; }
        public string PartyName { get; private set; }
        public string CurrencyId { get; private set; }
        public decimal FxRate { get; private set; }
        public decimal Amount { get; private set; }
        public NaicomRecord? Naicom { get; protected set; }
    }
}

