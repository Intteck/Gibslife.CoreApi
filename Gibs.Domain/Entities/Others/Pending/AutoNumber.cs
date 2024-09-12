using System.ComponentModel.DataAnnotations;

namespace Gibs.Domain.Entities
{
    public class AutoNumber
    {
        [Key]
        public string NumType { get; set; }
        [Key]
        public string? BranchID { get; set; }
        public string? RiskID { get; set; }
        public long? NextValue { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string? Format { get; set; }
        public string? Sample { get; set; }

    }

    public class PolicyAutoNumber
    {
        [Key]
        public string NumType { get; set; }
        [Key]
        public string BranchID { get; set; }
        [Key]
        public string RiskID { get; set; }
        //[Key]
        //public string CompanyID { get; set; } //removed 18 sept 23

        public long? NextValue { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string Format { get; set; }
        public string Sample { get; set; }
    }

}
