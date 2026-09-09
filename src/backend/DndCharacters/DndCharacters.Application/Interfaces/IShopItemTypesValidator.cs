using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Interfaces
{
    public interface IShopItemTypesValidator
    {
        bool IsValid(ShopType shopType, ItemType itemType);
    }
}
