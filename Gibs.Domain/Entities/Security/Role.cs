namespace Gibs.Domain.Entities
{
    public class Role : AuditRecord
    {
        public string Id { get; private set; }
        public string Name { get; private set; }

        //Navigation Properties
        public virtual ReadWriteList<Permission> Permissions { get; private set; } = [];

        protected Role() { /*EfCore*/ }

        public Role(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public void UpdateRoleName(string name)
        {
            Name = name;
        }

        public void ReplacePermissions(List<Permission> permissions)
        {
            Permissions.Clear();
            permissions.ForEach(Permissions.Add);
        }
    }
}