using System.Diagnostics.CodeAnalysis;

namespace Gibs.Domain.Entities
{
    public class SalesChannel
	{
        protected SalesChannel() { }

        [SetsRequiredMembers]
        public SalesChannel(string branchId, string id, string name, string? description)
        {
            BranchId = branchId;
            //ParentID = parentID;
            Id = id;
            Name = name;
            AltName = description;
        }

        public void Update(string name, string? description)
        {
            //Name = name;
            AltName = description;
        }

        public string? BranchId { get; private set; }
        public required string Id { get; init; }
        public required string Name { get; init; }
        public string? AltName { get; private set; }

        //Navigation Properties
        //public virtual Branch? Branch { get; private set; }
        //public virtual ReadOnlyList<SalesSubChannel> SubChannels { get; private set; } = [];
    }

    public class SalesSubChannel
    {
        protected SalesSubChannel() { }

        public required string ChannelId { get; init; }
        public required string Id { get; init; }
        public required string Name { get; init; }
        public string? AltName { get; private set; }

        //Navigation Properties
        //public virtual SalesChannel Channel { get; private set; } = null!;
    }

    public class Marketer : AuditRecord
    {
        public string Id { get; private set; }
        public string? ChannelId { get; private set; }
        public string? SubChannelId { get; private set; }
        public string FullName { get; private set; }
        public bool Active { get; private set; }

        //public DateTime? YearStart { get; private set; }
        //public DateTime? YearEnd { get; private set; }
        //public decimal? CurrentTarget { get; private set; }
        //public decimal? PreviousTarget { get; private set; }

        //Navigation Properties
        //public virtual SalesSubChannel Group { get; private set; }
        //public virtual SalesChannel Channel { get; private set; }

        protected Marketer() { /*EfCore*/ }

        public Marketer(string id, string fullName, string? channelId, string? subChannelId)
        {
            Id = id;
            Active = true;
            FullName = fullName;
            ChannelId = channelId;
            SubChannelId = subChannelId;
        }
    }
}
