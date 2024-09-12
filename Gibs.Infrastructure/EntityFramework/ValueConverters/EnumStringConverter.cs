using System.Reflection;
using System.Linq.Expressions;
using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Gibs.Domain.Entities;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    public class EnumStringConverter<TEnum> 
        : ValueConverter<TEnum, string> where TEnum : struct, Enum
    {
        private static readonly ConcurrentDictionary<string, string> _cache = new();
        private static readonly Expression<Func<TEnum, string>> toProviderExpression = x => ToDbString(x);
        private static readonly Expression<Func<string, TEnum>> fromProviderExpression = x => ToEnum(x);

        public EnumStringConverter() 
            : base(toProviderExpression, fromProviderExpression, null) { }

        private static string ToDbString(TEnum tEnum)
        {
            var cacheKey = $"{tEnum.GetType().FullName}.{tEnum}";

            return _cache.GetOrAdd(cacheKey, x =>
            {
                var dbAttribute = tEnum
                    .GetType()
                    .GetTypeInfo()
                    .GetField(tEnum.ToString())!
                    .GetCustomAttribute<GibsValueAttribute>(false);

                return dbAttribute != null ? dbAttribute.DbStrings[0] : tEnum.ToString();
            });
        }

        private static TEnum ToEnum(string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue))
                return default; //null;

            Type enumType = typeof(TEnum);

            stringValue = stringValue.ToUpper();
            var cacheKey = $"{enumType.FullName}.VALUE.{stringValue}";

            var enumValue = _cache.GetOrAdd(cacheKey, x =>
            {
                var cacheField = enumType
                .GetTypeInfo()
                .GetFields()
                .Select(field => (field, field.GetCustomAttribute<GibsValueAttribute>(false)))
                .Where(x => x.field.IsLiteral == true)
                .Where(x => (x.Item2 != null && x.Item2.DbStrings.Contains(stringValue)) ||
                            (x.field.GetValue(x.field)!.ToString() == stringValue))
                .Select(x => x.field)
                .FirstOrDefault();

                if (cacheField != null)
                    return cacheField.GetValue(cacheField)!.ToString()!;

                // the DB value does NOT match anything in the Enum
                throw new Exception($"{stringValue} value cannot be mapped to Enum {enumType.FullName}");
            });

            return (TEnum)Enum.Parse(enumType, enumValue);
        }
    }
}
