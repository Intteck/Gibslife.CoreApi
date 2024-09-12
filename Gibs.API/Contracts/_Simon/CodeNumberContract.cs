using Gibs.Domain.Entities;
using System;

namespace Gibs.Api.Contracts.V1
{
    public class UpdateCodeNumberRequest
    {
        public required string SerialIncrementField { get; init; }
        public required string SerialResetMode { get; init; }
        public required string Format { get; init; }
        public required string Sample { get; init; }
    }

    public class CodeNumberResponse(CodeNumber code)
    {
        public string SerialIncrementField { get; } = code.SerialIncrementField;
        public string SerialResetMode { get; } = code.SerialResetMode;
        public string Format { get; } = code.Format;
        public string Sample { get; } = code.Sample;
        public string CodeNumberID { get; } = code.CodeNumberID;
        public long NextValue { get; } = code.NextValue;
        public DateTime? LastUpdatedOn { get; } = code.LastUpdatedOn;
        public DateTime? LastResetOn { get; } = code.LastResetOn;
    }
}
