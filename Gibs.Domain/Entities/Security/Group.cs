namespace Gibs.Domain.Entities
{
    public class Group : AuditRecord
    {
        public string Id { get; private set; }
        public string Name { get; private set; }

        //Navigation Properties
        public virtual ReadWriteList<User> Users { get; private set; } = [];

        protected Group() { /*EfCore*/ }

        public Group(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public void UpdateGroupName(string name)
        {
            Name = name;
        }

        public void ReplaceUsers(List<User> newUsers)
        {
            Users.Clear();
            newUsers.ForEach(Users.Add);
        }
    }
}