using Gibs.Domain.Entities;

namespace Gibs.Api.Contracts.V1
{



    public class AccountMetadata(Account account)
    {
        public string AccountID { get; } = account.Id;
        public string AccountName { get; } = account.Name;
    }

}
