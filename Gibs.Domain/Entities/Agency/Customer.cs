namespace Gibs.Domain.Entities
{
    public class Customer : AgencyEntity<CustomerTypeEnum>
    {
        protected Customer() { /*EfCore*/ }

        public Customer(ICodeNumberFactory codeFactory, bool isOrg, 
            string title, string lastName, string firstName, string otherNames,
            string email, string address)
        {
            Id = codeFactory.CreateCodeNumber(CodeTypeEnum.INSURED); 
            Type = isOrg ? CustomerTypeEnum.CORPORATE : CustomerTypeEnum.INDIVIDUAL; 
            LastName = lastName;
            FirstName = firstName;
            OtherNames = otherNames;
            Title = title;
            Email = email;
            Address = address;

            FullName = lastName.Trim();

            if (firstName != null && !FullName.Contains(firstName))
                FullName += " " + firstName.Trim();

            if (otherNames != null && !FullName.Contains(otherNames))
                FullName += " " + otherNames.Trim();
		}

        public string? Title { get; private set; }
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string? OtherNames { get; private set; }
        //public GenderEnum? Gender { get; private set; }

        public string? Industry { get; private set; }
        public string? RiskProfile { get; private set; }
    }
}
