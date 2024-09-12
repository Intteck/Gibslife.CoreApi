using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Gibs.Domain.Entities;

namespace Gibs.Api.Contracts.V1
{
    public class CreatePolicyRequest : RenewPolicyRequest
    {
        [JsonPropertyOrder(-1)]
        public required string ProductID { get; init; }  
        [JsonPropertyOrder(-1)]
        public string? SubChannelID { get; set; }
        [JsonPropertyOrder(4)]
        public required CreateCustomerRequest Insured { get; init; }
        [JsonPropertyOrder(5), MinLength(1)]
        public required IEnumerable<PolicySectionContract> Sections { get; init; } = [];

        internal IEnumerable<SectionRecord> AsRecords()
        {
            return Sections.Select(x => x.AsRecord());
        }
    }

    public class RenewPolicyRequest
    {
        [Range(0, 365)] //not public yet
        internal int StandardCoverDays { get; init; } = 0; //Only apply pro-rata if this value > 0
     
        public required DateOnly StartDate { get; init; }
        public required DateOnly EndDate { get; init; }
        public string? CurrencyID { get; init; } = "NGN";
        public decimal? CurrencyRate { get; init; } = 1; 
        public required string? PartnerID { get; set; }
        public required string PaymentAccountID { get; init; } 
        public string? PaymentReferenceID { get; init; }
    }

    public class PolicyResponse
    {
        public PolicyResponse(PolicyHistory h)
        {
            PolicyNo = h.PolicyNo;
            DeclareNo = h.DeclareNo;
            EndorseNo = h.EndorsementNo;
            EndorseID = h.Endorsement.ToString();
            DebitNoteNo = h.DebitNoteNo;

            ProductID = h.Members.ProductId;
            ProductName = h.Members.ProductName;
            PartnerID = h.Members.PartyId;
            PartnerName = h.Members.PartyName;
            CustomerID = h.Members.CustomerId;
            CustomerName = h.Members.CustomerName;

            EntryDate = h.Business.TransDate;
            StartDate = h.Business.StartDate;
            EndDate = h.Business.EndDate;
            CurrencyID = h.Business.FxCurrencyId;
            CurrencyRate = h.Business.FxRate;
            SumInsured = h.Premium.SumInsured;
            Premium = h.Premium.GrossPremium;

            NaicomID = h.NaicomId;

            if (h.DebitNote != null) //simplify
                NaicomID = h.DebitNote.Naicom?.UniqueId; //use h.NaicomID

            if (h.Sections != null)
                Sections = h.Sections.Select(PolicySectionContract.Create);
        }

        public string PolicyNo { get; }
        public string DeclareNo { get; }
        public string EndorseNo { get; }
        public string EndorseID { get; }
        public string? NaicomID { get; }
        public string? DebitNoteNo { get; }

        public string ProductID { get; }
        public string ProductName { get; }
        public string CustomerID { get; }
        public string CustomerName { get; } 
        public string PartnerID { get; }
        public string PartnerName { get; }

        public DateTime EntryDate { get; }
        public DateOnly StartDate { get; }
        public DateOnly EndDate { get; }
        public decimal SumInsured { get; }
        public decimal Premium { get; }
        public string CurrencyID { get; }
        public decimal CurrencyRate { get; }
        public IEnumerable<PolicySectionContract>? Sections { get; }
    }
}
