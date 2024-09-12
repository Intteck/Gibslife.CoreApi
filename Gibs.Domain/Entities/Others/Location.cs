using System.Diagnostics.CodeAnalysis;

namespace Gibs.Domain.Entities
{
    public class Branch : AuditRecord
	{
		public string? RegionId { get; private set; }
        public string? StateId { get; private set; }
        public required string Id { get; init; }
		public required string Name { get; init; }
		public string? AltName { get; private set; }
		public string? Address { get; private set; }
		public string? Phone { get; private set; }
		public string? Tag { get; private set; }

        protected Branch() { /*EfCore*/ }

        [SetsRequiredMembers]
        public Branch(string id, string name, string? altName, string? stateId, string regionId)
        {
            Id = id;
            Name = name;
            AltName = altName;
            RegionId = regionId;
            StateId = stateId;
        }
    }

	public class Region : AuditRecord
	{
        public required string Id { get; init; }
		public required string Name { get; set; }
		public string? AltName { get; private set; }


        //Navigation 
        public ICollection<Branch> Branches { get; private set; } = [];

        protected Region() { /*EfCore*/ }

        [SetsRequiredMembers]
        public Region(string id, string name, string? remarks)
        {
            Id = id;
            Name = name;
            AltName = remarks;
        }

        public void UpdateRegionName(string name)
        {
            Name = name;
        }
    }

 //   public class County
	//{
 //       public required string StateId { get; init; }
 //       public required string Id { get; init; }
 //       public required string Name { get; init; }
 //   }

 //   public class State
	//{
 //       public required string CountryId { get; init; }
 //       public required string Id { get; init; }
 //       public required string Name { get; init; }
 //   }

 //   public class Country
	//{
 //       public required string Id { get; init; }
	//	public required string Name { get; init; }
	//}
}

