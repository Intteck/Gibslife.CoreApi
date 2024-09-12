namespace Gibs.Domain.Entities
{
    public class TemplateID
    {
        public string TemplateId { get; private set; }
        public TemplateTypeEnum TypeId { get; private set; }

        private TemplateID(string templateId, TemplateTypeEnum typeId)
        {
            TemplateId = templateId;
            TypeId = typeId;
        }

        public static TemplateID Create(string productId)
        {
            var typeId = TemplateTypeEnum.POLICYDOC;
            return new( $"{typeId}_{productId}", typeId);
        }

        public static TemplateID Create(string endorseId, string riskId, bool collective)
        {
            var typeId = TemplateTypeEnum.ENDORSEMENT;
            var templateId = $"{typeId}_{endorseId}_{riskId}";

            if (collective)
                templateId += "_C";

            return new (templateId, typeId);
        }

        public override string ToString()
        {
            return TemplateId; 
        }
    }
   
    public class TemplateDoc : AuditRecord
	{
        protected TemplateDoc() { }

        public TemplateDoc(TemplateID id, byte[] bytes, string contentType, string description)
        {
            ArgumentNullException.ThrowIfNull(id);

            Id = id.TemplateId;
            TypeId = id.TypeId;

            Description = description;
            Blob = new Blob(bytes, contentType, $"{Id} {Description}");
        }

        public void UpdateData(byte[] bytes, string contentType)
        {
            if (Blob == null)
                throw new InvalidOperationException("Template Blob was not yet loaded");

            Blob.UpdateBlob(bytes, contentType, Description);
        }

        public string Id { get; private set; }
        public TemplateTypeEnum TypeId { get; private set; }
        public Guid BlobId { get; private set; }
        public string Description { get; private set; }

        //Navigation Properties
        public Blob Blob { get; private set; }
    }

    public enum TemplateTypeEnum
    {
        POLICYDOC, ENDORSEMENT, INSPECTION
    }
}
