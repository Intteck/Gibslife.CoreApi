using System.ComponentModel.DataAnnotations;

namespace Gibs.Domain.Entities
{
    public class ReportGroup : AuditRecord
    {
        [Key]
        public string GroupID { get; private set; }
        [Required]
        public string GroupName { get; private set; }

        public string ReportType { get; private set; } // PL, BS, MPR etc

        //Navigation Properties
        public virtual ReadOnlyList<ControlAccount> ControlAccounts { get; private set; }

        protected ReportGroup() { /*EfCore*/ }

        public ReportGroup(
            string reportGroupID,
            string reportGroupName,
            string reportType)
        {
            GroupID = reportGroupID;
            GroupName = reportGroupName;
            ReportType = reportType;

        }
    }
}
