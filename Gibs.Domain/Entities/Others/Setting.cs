
namespace Gibs.Domain.Entities
{
    public class Setting : AuditRecord
	{
        public string Id { get; init; }
        public string Name { get; private set; }
        public string DataType { get; private set; }
        public string MinValue { get; private set; }
        public string MaxValue { get; private set; }
        public string DefValue { get; private set; }
        public string Value { get; private set; }

        protected Setting() { /*EfCore*/ }

        public Setting(string id, string name, string value) 
        {
            Id = id;
            Name = name;
            Value = value;
        }

        public void UpdateValue(string value)
        {
            Value = value;
        }

    }
}
