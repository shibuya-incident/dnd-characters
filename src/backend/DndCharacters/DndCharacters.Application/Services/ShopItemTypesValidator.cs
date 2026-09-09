using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Services
{
    public class ShopItemTypesValidator : IShopItemTypesValidator
    {
        public bool IsValid(ShopType shopType, ItemType itemType)
        {
            return shopType switch
            {
                ShopType.General => true,
                ShopType.Bookstore => itemType is ItemType.Book or ItemType.Scroll,
                ShopType.TailorShop => itemType is ItemType.Cloth or ItemType.Armor,
                ShopType.Blacksmith => itemType is ItemType.Weapon or ItemType.Armor,
                ShopType.Herbolary => itemType is ItemType.Potion or ItemType.Consumable,
                ShopType.MusicShop => itemType is ItemType.Instrument,
                _ => false,
            };
        }
    }
}
