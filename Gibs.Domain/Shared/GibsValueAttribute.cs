namespace Gibs.Domain.Entities
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class GibsValueAttribute(params string[] dbStrings) : Attribute
    {
        public string[] DbStrings
        {
            get { return dbStrings; }
        }
    }
}
