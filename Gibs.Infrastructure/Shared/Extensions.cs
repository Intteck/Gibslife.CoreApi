using Gibs.Infrastructure.EntityFramework.ValueGenerators;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Gibs.Infrastructure
{
    public static class Extensions
    {
        public static string ToSentenceCase(this string strIn, string spacer = " ")
        {
            return string.Concat(strIn.Select((c, i) => i > 0 && char.IsUpper(c) ? spacer + c.ToString() : c.ToString()));
        }

        public static bool IsNullableEnum(this Type type, [NotNullWhen(true)] out Type? uType)
        {
            uType = Nullable.GetUnderlyingType(type);
            return (uType != null) && uType.IsEnum;
        }

        public static PropertyBuilder<TProperty> HasValueOnAdd<TProperty>(
            this PropertyBuilder<TProperty> propertyBuilder, TProperty value)
            where TProperty : IConvertible
        {
            return propertyBuilder.HasValueGenerator((prop, entityType) 
                => new ConstantValueGenerator<TProperty>(value));
        }
    }
}