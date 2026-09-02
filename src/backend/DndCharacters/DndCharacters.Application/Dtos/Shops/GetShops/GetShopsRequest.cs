using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Shops.GetShops
{
    public class GetShopsRequest : PageListRequest<GetShopsSortByRequest>
    {
        //Filtering
        public string? Name { get; set; }
        public ShopType? ShopType { get; set; }
        public int? ItemsCount { get; set; }
    }
}
