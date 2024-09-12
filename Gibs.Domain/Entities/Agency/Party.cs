
namespace Gibs.Domain.Entities
{
	public class PartyType
	{
        public PartyType() { } //efcore

        public PartyType(string id, string name, string categoryId)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string CategoryId { get; private set; }

        //Navigation Properties
        public virtual ICollection<Party> Parties { get; private set; } = [];
    }

    public class Party : AgencyEntity<PartyType>
    {
		public Party() { } //efcore

		public Party(ICodeNumberFactory codeFactory, PartyType partyType, string fullName, string email, string phone)
		{
			Id = codeFactory.CreateCodeNumber(CodeTypeEnum.AGENTS); 
			Type = partyType;
			TypeId = partyType.Id;

			FullName = fullName; 
			Phone = phone;
			Email = email;
			CommTypeId = CommTypeEnum.INCLUDED;
		}

        public string TypeId { get; private set; }
        public CommTypeEnum CommTypeId { get; private set; }

        //Navigation Properties
        public virtual PartyType PartyType { get; private set; } 
    }
}
