using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Gibs.Infrastructure.EntityFramework.ValueGenerators
{
    public class ConstantValueGenerator<TValue> : ValueGenerator<TValue> where TValue : IConvertible
    {
        public override bool GeneratesTemporaryValues => false;

        private readonly TValue _value;

        public ConstantValueGenerator(TValue value)
        {
            _value = value;
        }
        
        public override TValue Next(EntityEntry entry) => _value;

        protected override object NextValue(EntityEntry entry) => _value;
    }
}
