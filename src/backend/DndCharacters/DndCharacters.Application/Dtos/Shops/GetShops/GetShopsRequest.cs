using DndCharacters.Application.Commons.Sorting;
using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Shops.GetShops
{
    public class GetShopsRequest
    {
        //Filtering
        public string? Name { get; set; }
        public ShopType? ShopType { get; set; }
        public int? ItemsCount { get; set; }

        //Sorting
        public GetShopsSortByRequest SortBy { get; set; } = GetShopsSortByRequest.CreatedAt;
        public SortDirection SortDirection { get; set; } = SortDirection.Desc;

        //Pagination
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 20;
    }
}
