using Gibs.Domain.Entities;

namespace Gibs.Domain
{
    public static partial class Extensions
    {
        public static byte[] ToBytes(this Stream stream)
        {
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        public static bool EqualsIgnoreCase(this string? str1, string? str2)
        {
            return string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
        }

        public static IEnumerable<TSource> DuplicatesBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.GroupBy(keySelector)
                         .Where(g => g.Skip(1).Any())
                         .SelectMany(c => c);
        }

        public static List<string> ToGibsDbStrings<T>(this T source)
            where T : IComparable, IFormattable, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("en must be enum type");

            var fieldName = source.ToString()!;
            var fi = source.GetType().GetField(fieldName);

            if (fi == null)
                return new List<string>() { fieldName };

            var attributes = (GibsValueAttribute[])fi.GetCustomAttributes(
                typeof(GibsValueAttribute), false);

            var list = new List<string>() { fieldName };

            if (attributes != null && attributes.Length > 0)
                list.AddRange(attributes[0].DbStrings);

            return list;
        }
    }
}
