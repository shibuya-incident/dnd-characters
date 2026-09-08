using DndCharacters.Domain.Entities;

namespace DndCharacters.Infrastructure.Persistence.DataModels
{
    public sealed record ShopItemQueryModel(
        ShopItem ShopItem,
        Item Item);
}
