using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Shops.GetShopItems
{
    public class GetShopItemsRequest : PageListRequest<GetShopItemsSortByRequest>
    {
        public string? Name { get; set; }
        public ItemType? ItemType { get; set; }
        public int? Stock { get; set; }
        public decimal? Price { get; set; }
    }
}
