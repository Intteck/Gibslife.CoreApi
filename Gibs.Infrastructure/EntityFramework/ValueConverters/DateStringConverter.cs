using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gibs.Infrastructure.EntityFramework.Configuration
{
    public class DateStringConverter : ValueConverter<DateTime?, string>
    {
        public DateStringConverter()
            : base(toProviderExpression, fromProviderExpression)
        { }

        private static readonly Expression<Func<DateTime?, string>> toProviderExpression = x => ToDbString(x);
        private static readonly Expression<Func<string, DateTime?>> fromProviderExpression = x => ToDateTime(x);

        public static string ToDbString(DateTime? dateTime)
        {
            //2022-12-13 03:39:33.0198631 (Field42 in PolicyHistory)
            return dateTime == null ? string.Empty 
                : dateTime.Value.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
        }

        public static DateTime? ToDateTime(string stringValue)
        {
            if (DateTime.TryParse(stringValue, out var dateTime))
                return dateTime;

            return null;
        }
    }
}
