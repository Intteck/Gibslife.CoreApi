
namespace Gibs.Domain.Entities
{
    public class BusinessMembers
    {
        protected BusinessMembers() { /*EfCore*/ }

        public BusinessMembers(Product product, Customer customer, Branch branch, Party party, SalesChannel channel, SalesSubChannel subChannel, Marketer? marketer)
        {
            ArgumentNullException.ThrowIfNull(product);
            ArgumentNullException.ThrowIfNull(customer);
            ArgumentNullException.ThrowIfNull(branch);
            ArgumentNullException.ThrowIfNull(party);
            //ArgumentNullException.ThrowIfNull(marketer);
            ArgumentNullException.ThrowIfNull(channel);
            ArgumentNullException.ThrowIfNull(subChannel);

            ProductId = product.Id;
            ProductName = product.Name;

            CustomerId = customer.Id;
            CustomerName = customer.FullName;

            BranchId = branch.Id;
            BranchName = branch.Name;

            PartyId = party.Id;
            PartyName = party.FullName;

            //if (leadParty != null)
            //{
            //    LeadPartyId = leadParty.Id;
            //    LeadPartyName = leadParty.FullName;
            //}

            if (marketer  != null)
            {
                MarketerId = marketer.Id;
                MarketerName = marketer.FullName;
            }

            ChannelId = channel.Id;
            ChannelName = channel.Name;

            SubChannelId = subChannel.ChannelId;
            SubChannelName = subChannel.Name;
        }

        public string ProductId { get; protected set; }
        public string ProductName { get; protected set; }

        public string CustomerId { get; protected set; }
        public string CustomerName { get; protected set; }

        public string BranchId { get; protected set; }
        public string BranchName { get; protected set; }

        public string PartyId { get; protected set; }
        public string PartyName { get; protected set; }

        //public string? LeadPartyId { get; protected set; }
        //public string? LeadPartyName { get; protected set; }

        public string? MarketerId { get; protected set; }
        public string? MarketerName { get; protected set; }

        public string? ChannelId { get; protected set; }
        public string? ChannelName { get; protected set; }

        public string? SubChannelId { get; protected set; }
        public string? SubChannelName { get; protected set; }

        public BusinessMembers ShallowCopy()
        {
            return (BusinessMembers)MemberwiseClone();
        }
    }
}
