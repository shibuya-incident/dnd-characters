using DndCharacters.Application.Commons.Sorting;

namespace DndCharacters.Application.Dtos.Shops.GetShops
{
    public class GetShopsRequest
    {
        public GetShopsSortByRequest SortBy { get; set; } = GetShopsSortByRequest.CreatedAt;
        public SortDirection SortDirection { get; set; } = SortDirection.Desc;
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 20;
    }
}
