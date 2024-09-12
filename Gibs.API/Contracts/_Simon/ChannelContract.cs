using Gibs.Domain.Entities;

namespace Gibs.Api.Contracts.V1
{
    public class CreateChannelRequest : UpdateChannelRequest
    {
        public required string ChannelID { get; init; }
    }

    public class UpdateChannelRequest
    {
        public required string BranchID { get; init; }
        public required string ChannelName { get; init; }
        public string? ChannelAltName { get; init; }
    }

    public class ChannelResponse(SalesChannel channel)
    {
        public string BranchID { get; } = channel.BranchId ?? string.Empty;
        public string ChannelID { get; } = channel.Id;
        public string ChannelName { get; } = channel.Name;
        public string? ChannelAltName { get; } = channel.AltName;
        //public SubChannelResponse[] SubChannels { get; } = [];
    }



    //public class CreateSubChannelRequest : UpdateSubChannelRequest
    //{
    //    public required string SubChannelID { get; init; }
    //}

    //public class UpdateSubChannelRequest
    //{
    //    public required string ChannelID { get; init; }
    //    public required string SubChannelName { get; init; }
    //    public string? SubChannelAltName { get; init; }
    //}

    //public class SubChannelResponse(SalesSubChannel subChannel)
    //{
    //    public string SubChannelID { get; } = subChannel.Id;
    //    public string SubChannelName { get; } = subChannel.Name;
    //    public string? SubChannelAltName { get; } = subChannel.AltName;
    //}
}
