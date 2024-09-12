using Gibs.Domain.Entities;

namespace Gibs.Api.Contracts.V1
{
    public class UpdateSettingRequest
    {
        public required string SettingID { get; init; }
        public required string Value { get; init; }
    }

    public class SettingResponse(Setting setting)
    {
        public string SettingID { get; } = setting.Id;
        public string Description { get; } = setting.Name;
        public string MinValue { get; } = setting.MinValue;
        public string MaxValue { get; } = setting.MaxValue;
        public string Value { get; } = setting.Value;
    }
}
