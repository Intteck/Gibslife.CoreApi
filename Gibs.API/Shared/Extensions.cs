using Gibs.Domain.Entities;
using System;

namespace Gibs.Api
{
    public static partial class Extensions
    {
        public static DateTime ToDateTime(this DateOnly dateOnly)
        {
            var time = new TimeOnly(0);
            return dateOnly.ToDateTime(time);
        }
        public static DateTime? ToDateTime(this DateOnly? dateOnly)
        {
            if (!dateOnly.HasValue)
                return null;

            return dateOnly.Value.ToDateTime();
        }

        public static DateOnly ToDateOnly(this DateTime dateTime)
        {
            return DateOnly.FromDateTime(dateTime);
        }
        public static DateOnly? ToDateOnly(this DateTime? dateTime)
        {
            if (!dateTime.HasValue) 
                return null;

            return  DateOnly.FromDateTime(dateTime.Value);
        }
        public static Type? UnderlyingType(this Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                return Nullable.GetUnderlyingType(type);

            return type;
        }

    }
}
