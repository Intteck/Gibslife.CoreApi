namespace Gibs.Domain.Entities
{
    public class Account : AuditRecord
    {
        public required string Id { get; init; }
        public required string Name { get; init; }
        public required string ControlId { get; init; }
        public string? Remarks { get; private set; }

        //Navigation Properties
        //public virtual ControlAccount Control { get; private set; } 

        protected Account() { /*EfCore*/ }

        public Account(
            //ControlAccount controlAccount,
            string id, 
            string name,
            string remarks)
        {
            //ControlAccount = controlAccount;
            Id = id;
            Name = name;
            Remarks = remarks;
        }
    }
}
