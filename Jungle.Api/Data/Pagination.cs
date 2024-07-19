using Microsoft.EntityFrameworkCore;

namespace Jungle.Api.Data
{
    public class Pagination<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<T>? Data { get; set; }

        public Pagination(int page, int pageSize, int totalCount, IEnumerable<T> data)
        {
            Page = page;
            PageSize = pageSize;
            TotalCount = totalCount;
            Data = data;
        }

        public bool HasPrevious => Page > 1;
        public bool HasNext => Page < TotalCount / PageSize;

        public static async Task<Pagination<T>> ToPagedListAsync(IQueryable<T> source, int page, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new Pagination<T>(page, pageSize, count, items);
        }
    }
}
