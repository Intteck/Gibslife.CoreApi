using System;
using System.Diagnostics.CodeAnalysis;
using Gibs.Domain.Entities;

namespace Gibs.Api.Contracts.V1
{
    public class CreatePartnerRequest : PersonRequest
    {
        public required string TypeID { get; init; }
        public required string FullName { get; init; }
    }

    public class UpdatePartnerRequest : CreatePartnerRequest
    {
    }

    public class PartnerResponse : CreatePartnerRequest
    {
        [SetsRequiredMembers]
        public PartnerResponse(Party party)
        {
            PartnerID = party.Id;
            TypeID = party.TypeId;
            FullName = party.FullName;

            //Gender = party.Gender;
            Email = party.Email;
            Phone = party.Phone!;
            PhoneAlt = party.PhoneAlt;
            Street = party.Address;
            CityLGA = party.CityLGA;
            StateID = party.StateId;
            CountryID = party.Country;

            TaxNumber = party.TaxNumber;
            KycNumber = party.KycNumber;
            KycTypeID = party.KycTypeId ?? KycTypeEnum.NOT_AVAILABLE;
            KycIssueDate =  party.KycIssueDate;
            KycExpiryDate = party.KycExpiryDate;
        }

        public string PartnerID { get; }
        public bool IsActive { get; }
    }

    public class PersonRequest
    {
        public string? Email { get; init; }
        public string? Phone { get; init; }
        public string? PhoneAlt { get; init; }
        public string? Street { get; init; }
        public string? CityLGA { get; init; }
        public string? StateID { get; init; }
        public string? CountryID { get; init; }

        public string? TaxNumber { get; init; }
        public string? KycNumber { get; init; }
        public KycTypeEnum KycTypeID { get; init; }
        //public string KycTypeID { get; init; }
        public DateOnly? KycIssueDate { get; init; }
        public DateOnly? KycExpiryDate { get; init; }
    }

}
