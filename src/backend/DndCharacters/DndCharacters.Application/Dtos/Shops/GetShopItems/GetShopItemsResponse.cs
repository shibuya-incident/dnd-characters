
namespace DndCharacters.Application.Dtos.Shops.GetShopItems
{
    public record GetShopItemsResponse
    {
        public IReadOnlyCollection<GetShopItemsListItemResponse> Items { get; set; } = [];
    }
}
