
namespace Gibs.Api.Contracts.V1
{
    public class DateRangeQuery<TRange> where TRange : struct
    {
        public DateRangeQuery() { }

        public TRange? DateFrom { get; set; }

        public TRange? DateTo { get; set; }

        internal bool CanSearch => DateFrom.HasValue && DateTo.HasValue;
    }
}
