using System;
using System.Diagnostics.CodeAnalysis;
using Gibs.Domain.Entities;

namespace Gibs.Api.Contracts.V1
{
    public class CreateCustomerRequest : PersonRequest
    {
        public string? Title { get; init; }
        public required string LastName { get; init; }
        public required string FirstName { get; init; }
        public string? OtherNames { get; init; }

        public required string FullName { get; init; }
        public DateOnly? BirthDate { get; init; }
    }

    public class UpdateCustomerRequest : CreateCustomerRequest
    {
    }

    public class CustomerResponse : CreateCustomerRequest
    {
        [SetsRequiredMembers]
        public CustomerResponse(Customer customer)
        {
            CustomerID = customer.Id;

            //if (customer.KycTypeId == KycTypeEnum.COMPANY_REG_NO)
            //{
            //    FullName = customer.FullName;
            //    BirthDate = customer.BirthDate;
            //    OrgRegNumber = customer.KycNumber;
            //}

            Title = customer.Title;
            LastName = customer.LastName;
            FirstName = customer.FirstName;
            OtherNames = customer.OtherNames;

            FullName = customer.FullName;   
            BirthDate = customer.BirthDate;

            Email = customer.Email;
            Phone = customer.Phone!;
            PhoneAlt = customer.PhoneAlt;
            Street = customer.Address;
            CityLGA = customer.CityLGA;
            StateID = customer.StateId;
            CountryID = customer.Country;

            TaxNumber = customer.TaxNumber;
            KycNumber = customer.KycNumber;
            KycTypeID = customer.KycTypeId ?? KycTypeEnum.NOT_AVAILABLE;
            KycIssueDate = customer.KycIssueDate;
            KycExpiryDate = customer.KycExpiryDate;
        }

        public string CustomerID { get; } 
    }
}
