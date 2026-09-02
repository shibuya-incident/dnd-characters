using DndCharacters.Application.Commons.Sorting;
using DndCharacters.Application.Constants;

namespace DndCharacters.Application.Commons.Pagination
{
    public class PageListRequest<TSortBy> where TSortBy : Enum
    {
        public required TSortBy SortBy { get; set; }
        public SortDirection SortDirection { get; set; } = PaginationConstants.DefaultSortDirection;
        public int Page { get; init; } = PaginationConstants.DefaultPage;
        public int PageSize { get; init; } = PaginationConstants.DefaultPageSize;
    }
}
