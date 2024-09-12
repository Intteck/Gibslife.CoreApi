using Gibs.Domain.Entities;
using System;

namespace Gibs.Api.Contracts.V1
{
    public class AuditLogResponse(AuditLog log)
    {
        public long LogID { get; } = log.Id;
        public string Source { get; } = log.Category;
        public string RowID { get; } = log.ModuleId;
        public string UserID { get; } = log.UserId;
        public string Action { get; } = log.ActionId.ToString();
        public string Description { get; } = log.Description;
        public string IpAddress { get; } = log.IpAddress;
        public DateTime AuditTime { get; } = log.EntryTimeUtc;
    }
}
