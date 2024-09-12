using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gibs.Domain.Entities
{
    public class UserSignature : AuditRecord
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SignatureID { get; private set; }
        [Required]
        public byte[] Signature { get; private set; }

        public string Remarks { get; private set; }

        //Navigation Properties
        //public virtual User User { get; private set; }

        protected UserSignature() { /*EfCore*/ }

        public UserSignature(byte[] signature, string remarks)
        {
            Signature = signature;
            Remarks = remarks;
        }
    }
}