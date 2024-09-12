using System.Diagnostics.CodeAnalysis;

namespace Gibs.Domain.Entities
{
    public class Signature 
    {
        public int Id { get; private set; }
        public string UserId { get; private set; }
        public Guid BlobId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Description { get; private set; }

        //Navigation Properties
        public User User { get; private set; }
        public Blob Blob { get; private set; }

        protected Signature() { /*EfCore*/ }

        public Signature(string description, Blob blob)
        {
            ArgumentNullException.ThrowIfNull(blob);
            ArgumentException.ThrowIfNullOrEmpty(description);

            CreatedAt = blob.CreatedAt;
            Description = description;
            BlobId = blob.Id;
            Blob = blob;
        }
    }

    public class Blob
    {
        public Guid Id { get; private set; }
        public byte[] Data { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Description { get; private set; }
        public string ContentType { get; private set; }

        //Navigation Properties

        protected Blob() { /*EfCore*/ }

        public Blob(byte[] data, string contentType, string description)
        {
            Id = Guid.NewGuid();
            UpdateBlob(data, contentType, description);
        }

        [MemberNotNull(nameof(Data), nameof(Description), nameof(ContentType))]
        public void UpdateBlob(byte[] data, string contentType, string description)
        {
            ArgumentNullException.ThrowIfNull(data);
            ArgumentException.ThrowIfNullOrEmpty(contentType);
            ArgumentException.ThrowIfNullOrEmpty(description);

            Data = data;
            Description = description;
            ContentType = contentType;
            CreatedAt = DateTime.UtcNow;
        }
    }
}