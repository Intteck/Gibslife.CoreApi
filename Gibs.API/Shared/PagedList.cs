using System.Linq;
using System.Threading.Tasks;
using Gibs.Api.Contracts.V1;
using Microsoft.EntityFrameworkCore;

namespace System.Collections.Generic
{
    public class PagedList<T> : List<T>
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalItems { get; private set; }
        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;

        internal PagedList(List<T> items, int total, int pageNumber, int pageSize)
        {
            TotalItems = total;
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling((decimal)total / pageSize);
            AddRange(items);
        }
    }

    public static class PagedListExtensions
    {
        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, PaginationQuery pg)
        {
            var total = source.Count();
            var items = await source.Skip(pg.SkipCount).Take(pg.PageSize).ToListAsync();
            return new PagedList<T>(items, total, pg.PageNumber, pg.PageSize);
        }

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, PaginationQuery pg)
        {
            var total = source.Count();
            var items = source.Skip(pg.SkipCount).Take(pg.PageSize).ToList();
            return new PagedList<T>(items, total, pg.PageNumber, pg.PageSize);
        }
    }
}
