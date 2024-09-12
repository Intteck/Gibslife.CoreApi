using System.ComponentModel.DataAnnotations;

namespace Gibs.Domain.Entities
{
    public class Deduction
    {
        [Key]
        public string DeductionID { get; private set; }
        [Required]
        public string DeductionName { get; private set; }
        [Required]
        public Account LedgerAccount { get; private set; }
        [Required]
        public decimal Rate { get; private set; }

        public string Narration { get; private set; }

        public string Remarks { get; private set; }
    }

}
