
namespace Gibs.Domain.Entities
{
    public class User : AuditRecord
    {
        protected User() { /*EfCore*/ }

        public User(string id, string password, string firstName, string lastName, string phone, string email)
        {
            Id = id;
            ApiKey = Guid.NewGuid().ToString();
            PasswordHash = EncodeToHash(password);
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            Email = email;
            IsActive = true;
        }

        public bool IsPasswordValid(string password)
        {
            return PasswordHash == EncodeToHash(password);
        }

        public void UpdateLastLoginDate()
        {
            if (ApiKey == null) ResetApiKey();

            LastLoginUtc = DateTime.UtcNow;
        }

        public void UpdateStatus(bool active)
        {
            IsActive = active;
        }

        public void UpdateProfile(string firstName, string lastName, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
        }

        public void UpdateGroups(List<Group> newGroups)
        {
            Groups.Clear();
            newGroups.ForEach(Groups.Add);
        }

        public void ChangePassword(string oldPassword, string newPassword, int nextExpiryDays = 60)
        {
            if (PasswordHash == EncodeToHash(oldPassword))
            {
                PasswordHash = EncodeToHash(newPassword);
                PasswordExpiryUtc = DateTime.UtcNow.AddDays(nextExpiryDays);
                return;
            }
            throw new Exception("Your old password is incorrect");
        }

        private static string EncodeToHash(string input)
        {
            // Use input string to calculate MD5 hash
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = System.Security.Cryptography.MD5.HashData(inputBytes);

            return Convert.ToHexString(hashBytes);
        }

        public string ResetPassword(TimeSpan? nextExpiry = null)
        {
            if (nextExpiry == null) 
                nextExpiry = TimeSpan.FromDays(60);

            var newPassword = "123456";// DateTime.Now.Ticks.ToString();

            PasswordHash = EncodeToHash(newPassword);
            PasswordExpiryUtc = DateTime.UtcNow.Add(nextExpiry.Value);

            return newPassword;
        }

        public void ResetApiKey()
        {
            ApiKey = Guid.NewGuid().ToString();
        }

        public bool HasPermission(PermissionEnum p)
        {
            //string permission = p.ToString();
            //List<Permission> result = CombinedPermissions.Where(x => x.PermissionID == permission).ToList();

            //if (result.Count > 0) return true;
            return false;
        }

        public string FullName => $"{FirstName}, {LastName.ToUpper()}";
        public bool IsPasswordExpired => false;// PasswordExpiryUtc > DateTime.UtcNow;


        public string Id { get; private set; }
        public string PasswordHash { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string? Address { get; private set; }
        public string? StaffNo { get; private set; }
        public string? AvatarUrl { get; private set; } = "https://th.bing.com/th/id/OIP.R4gEjzGdxIEBZ42eafUHfgAAAA?rs=1&pid=ImgDetMain";
        public string? Remarks { get; private set; }
        public DateTime? PasswordExpiryUtc { get; private set; }
        public DateTime? LastLoginUtc { get; private set; }
        public bool IsActive { get; private set; }
        public string ApiKey { get; private set; } = Guid.NewGuid().ToString();

        //Navigation Properties
        public virtual ReadWriteList<Group> Groups { get; private set; } = [];
        public virtual ReadWriteList<Approval> Approvals { get; private set; } = [];
        public virtual ReadWriteList<Signature> Signatures { get; private set; } = [];
    }
}