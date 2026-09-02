namespace DndCharacters.Application.Commons.Pagination
{
    public class PagedListResponse<TListItemResponse> where TListItemResponse : class
    {
        public IReadOnlyCollection<TListItemResponse> Items { get; set; } = [];
        public int Page { get; init; }
        public int PageSize { get; init; }
        public int TotalCount { get; set; }

        public bool HasNextPage => Page * PageSize < TotalCount;
        public bool HasPreviousPage => Page > 1;

    }
}
