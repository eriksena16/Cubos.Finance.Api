using Microsoft.EntityFrameworkCore;

namespace Cubos.Finance.Shared
{
    public static class IQueryableExtensions
    {
        public static async Task<QueryBaseResponse<T>> ResponseAsync<T>(this IQueryable<T> query, IQueryObject<T> filter)
        {
            var totalItems = await query.CountAsync();

            query = query.ApplyPaging(filter);

            var response = new QueryBaseResponse<T>(filter)
            {
                Items = await query.ToListAsync(),
                TotalItems = totalItems
            };

            return response;
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject<T> filter)
        {
            if (filter.CurrentPage <= 0)
                filter.CurrentPage = 1;

            if (filter.ItemsPerPage <= 0)
                filter.ItemsPerPage = 25;

            return query.Skip((filter.CurrentPage - 1) * filter.ItemsPerPage).Take(filter.ItemsPerPage);
        }
    }
}
