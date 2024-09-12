using Gibs.Domain.Entities;

namespace Gibs.Api.Contracts.V1
{
    public class CreateMarketerRequest
    {
        public required string MarketerID { get; init; }
        public required string FullName { get; init; }
        public string? ChannelID { get; init; }
        public string? SubChannelID { get; init; }
    }

    public class UpdateMarketerRequest
    {
        public string? ChannelID { get; init; }
        public string? SubChannelID { get; init; }
        //public DateTime? YearStart { get; init;}
        //public DateTime? YearEnd { get; init; }
        //public decimal? CurrentTarget { get; init; }
    }

    public class MarketerResponse(Marketer marketer)
    {
        public string MarketerID { get; } = marketer.Id;
        public string FullName { get; } =  marketer.FullName;
        public string? ChannelID { get; } = marketer.ChannelId;
        public string? SubChannelID { get; } = marketer.SubChannelId;
        //public DateTime? YearStart { get; } = marketer.YearStart;
        //public DateTime? YearEnd { get; } = marketer.YearEnd;
        //public decimal? CurrentTarget { get; } = marketer.CurrentTarget;
        //public decimal? PreviousTarget { get; } = marketer.PreviousTarget;
    }
}
