using Gibs.Domain.Entities;
using System;

namespace Gibs.Api.Contracts.V1
{
    public class TemplateResponse(TemplateDoc doc)
    {
        public string TypeID { get; } = doc.TypeId.ToString();
        public string TemplateID { get; } = doc.Id;

        public DateTime CreatedAt { get; } = doc.CreatedUtc;
        public string CreatedBy { get; } = doc.CreatedBy;
        public DateTime? UpdatedAt { get; } = doc.LastModifiedUtc;
        public string? UpdatedBy { get; } = doc.LastModifiedBy;
    }
}
