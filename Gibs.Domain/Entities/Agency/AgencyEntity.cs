
namespace Gibs.Domain.Entities
{
    public abstract class AgencyEntity<TEntityType> : AuditRecord
	{
		public string Id { get; protected set; }
		public string FullName { get; protected set; }
		public TEntityType Type { get; protected set; }
		public DateOnly? BirthDate { get; protected set; }

		public string Email { get; protected set; }
		public string? Phone { get; protected set; }
		public string? PhoneAlt { get; protected set; }

		public string? Address { get; protected set; }
		public string? CityLGA { get; protected set; }
		public string? StateId { get; protected set; }
		public string? Country { get; protected set; }

		public string? NextOfKin { get; protected set; }

        public string? TaxNumber { get; protected set; }
		public string? KycNumber { get; protected set; }
        public KycTypeEnum? KycTypeId { get; protected set; }
        public DateOnly? KycIssueDate { get; protected set; }
        public DateOnly? KycExpiryDate { get; protected set; }

		public AgencyEntity() { } //ef-core

		public void UpdatePersonal(string? nationality, DateOnly? dateOfBirth, string? nextOfKin)
		{
			Country = nationality;
			NextOfKin = nextOfKin;
			BirthDate = dateOfBirth;
		}

		public void UpdateContact(string? address, string? cityLGA, string? stateId, string? phone, string? phoneAlt)
		{
			Address = address;
			CityLGA = cityLGA;
			StateId = stateId;
			Phone = phone;
			PhoneAlt = phoneAlt;
		}

        public void UpdateKyc(string? taxNumber, KycTypeEnum? kycType, string? kycNumber, DateOnly? kycIssueDate, DateOnly? kycExpiryDate)
        {
            TaxNumber = taxNumber;
            KycTypeId = kycType;
            KycNumber = kycNumber;
            KycIssueDate = kycIssueDate;
            KycExpiryDate = kycExpiryDate;
        }

    }
}
