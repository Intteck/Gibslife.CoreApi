
namespace Gibs.Api.Contracts.V1
{
    internal class RangeQuery<TRange>(TRange? from, TRange? to) where TRange : struct
    {
        public TRange? From { get; init; } = from;

        public TRange? To { get; init; } = to;

        internal bool CanSearch => From.HasValue && To.HasValue;
    }
}
