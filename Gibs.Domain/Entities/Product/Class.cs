
namespace Gibs.Domain.Entities
{
    public class Class
	{
        public string Id { get; private set; }
        public string Name { get; private set; }

		//Navigation Properties
        public virtual ReadWriteList<MidClass> MidClasses { get; private set; }
        public virtual ReadWriteList<Product> Products { get; private set; }

        protected Class() { /*EfCore*/ }
    }

	public class MidClass
	{
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string ClassId { get; private set; }

		//Navigation Properties
        public ReadWriteList<Product> Products { get; private set; }

        protected MidClass() { /*EfCore*/ }

        public MidClass(Class @class, string id, string name)
        {
            //Class = @class;
            ClassId = @class.Id;
            Id = id;
            Name = name;
            Products = [];
        }

        public void UpdateName(string name)
        {
            Name = name;
        }
    }
}
