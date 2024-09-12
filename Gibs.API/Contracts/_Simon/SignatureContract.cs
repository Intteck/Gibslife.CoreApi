using System;
using Gibs.Domain.Entities;

namespace Gibs.Api.Contracts.V1
{
    public class SignatureResponse(Signature signature)
    {
        public string UserID { get; } = signature.UserId;
        public Guid SignatureID { get; } = signature.BlobId;
        public DateTime CreatedDate { get; } = signature.CreatedAt;
        public string Description { get; } = signature.Description;
        public string FileUrl { get; } = $"/media/{signature.BlobId}";

        //private static string GetUrl(HttpRequest req, Guid signatureId)
        //{
        //    return $"{req.Scheme}://{req.Host}/media/{signatureId}";
        //}
    }
}
