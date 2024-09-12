using System;
using System.Collections.Generic;

namespace Gibs.Api.Contracts.V1
{
    public class QuotePolicyRequest
    {
        public required string ProductID { get; init; }
        public required DateTime? EntryDate { get; init; }
        public required DateOnly? StartDate { get; init; }
        public required DateOnly? EndDate { get; init; }

        public string? FxCurrencyID { get; init; }
        public decimal? FxRate { get; init; } // NGN = 1

        public IEnumerable<QuoteSectionContract> Sections { get; init; } = [];
    }

    public class QuotePolicyResponse(QuoteValue total, QuoteValue share, QuoteValue prorated, IEnumerable<QuoteStep> steps)
    {
        public QuoteValue Total { get; } = total;
        public QuoteValue Share { get; } = share;
        public QuoteValue Prorated { get; } = prorated;

        public IEnumerable<QuoteStep> Steps { get; } = steps;
    }

    public class QuoteValue(decimal sumInsured, decimal premium)
    {
        public decimal SumInsured { get; } = sumInsured;
        public decimal Premium { get; } = premium;
    }

    public class QuoteStep
    {
        public int QuoteSN { get; }
        public string QuoteItem { get; } = string.Empty;
        public string ItemType { get; } = string.Empty; //enum => DISCOUNT, LOADING, PREMIUM, MULTIPLIER, etc
        public decimal Forward { get; } //broughtForward
        public decimal Rate { get; }    //percentage in decimal
        public decimal Amount { get; }  //Forward * Rate
        public decimal Balance { get; } //running Balance
    }

}
