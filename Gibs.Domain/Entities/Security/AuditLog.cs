
namespace Gibs.Domain.Entities
{
    public class AuditLog
    {
        public long Id { get; private set; }
        public DateTime EntryTimeUtc { get; private set; }
        public string UserId { get; private set; }
        public AuditActionEnum ActionId { get; private set; }
        public string Category { get; private set; }
        public string ModuleId { get; private set; }
        public string Description { get; private set; }
        public string IpAddress { get; private set; }

        protected AuditLog() { /*EfCore*/ }

        public AuditLog(AuditActionEnum actionId, string moduleId, string category, 
            string description, string ipAddress, string userId)
        {
            //Me.AuditLogID = [identity]
            EntryTimeUtc = DateTime.UtcNow;
            ActionId = actionId;
            ModuleId = moduleId;
            Category = category;
            Description = description;
            IpAddress = ipAddress;
            UserId = userId;
        }
    }

    public enum AuditActionEnum : byte
    {
        CREATE,
        UPDATE,
        DELETE,
        LOGIN,
        EDIT,

        //wasiu why?
        //use messaging log
        SMS_ALERT,
        NIID_ALERT,
        TREATY,
    }
}
