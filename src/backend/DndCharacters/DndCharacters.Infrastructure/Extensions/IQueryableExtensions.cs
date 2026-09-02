using DndCharacters.Application.Commons.Sorting;
using System.Linq.Expressions;

namespace DndCharacters.Infrastructure.Extensions
{
    internal static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySortDirection<T>(
            this IQueryable<T> query,
            SortDirection sortDirection,
            Expression<Func<T, object>> orderBy)
        {
            return sortDirection == SortDirection.Asc
                 ? query.OrderBy(orderBy)
                 : query.OrderByDescending(orderBy);

        }

        public static IQueryable<T> ApplyPagination<T>(
            this IQueryable<T> query,
            int page,
            int pageSize)
        {
            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
