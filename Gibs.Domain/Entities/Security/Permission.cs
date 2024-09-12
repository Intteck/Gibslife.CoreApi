
namespace Gibs.Domain.Entities
{
    public class Permission
    {
        public string Id { get; private set; }
        public string Name { get; private set; }

        //Navigation Properties
        public virtual ReadWriteList<Role> Roles { get; private set; } = [];
        //public virtual ReadWriteList<User> Users { get; private set; } = [];

        protected Permission() { /*EfCore*/ }
    }
}