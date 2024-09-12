namespace Gibs.Domain.Entities
{
    public class ControlAccount : AuditRecord
    {
        public required string Id { get; init; }
        public required string Name { get; init; }
        public AccountCategoryEnum Category { get; private set; }
        public EntryTypeEnum EntryType { get; private set; }
        public string? Remarks { get; private set; }

        //Navigation Properties
        //public virtual ReadWriteList<ReportGroup> Groups { get; private set; }
        public virtual ReadWriteList<Account> Accounts { get; private set; } = [];

        protected ControlAccount() { /*EfCore*/ }

        public ControlAccount(AccountCategoryEnum type, string id, string name) 
        {
            //this.Group = ledgerGroup;
            Id = id;
            Name = name;
            Category = type;

            switch (type)
            {
                case AccountCategoryEnum.ASSET:
                case AccountCategoryEnum.EXPENSE:
                    EntryType = EntryTypeEnum.DEBIT;
                    break;
                case AccountCategoryEnum.INCOME:
                case AccountCategoryEnum.LIABILITY:
                case AccountCategoryEnum.CAPITAL:
                    EntryType = EntryTypeEnum.CREDIT;
                    break;
            }
        }

        //public void SetReportGroups(
        //    ReadWriteList<ReportGroup> reportGroups)
        //{
        //    this.Groups = reportGroups;
        //}
    }

    public enum AccountCategoryEnum : byte
    {
        INCOME = 1,
        EXPENSE = 2,
        ASSET = 3,
        LIABILITY = 4,
        CAPITAL = 5
    }

    public enum EntryTypeEnum : byte
    {
        DEBIT,
        CREDIT
    }
}
