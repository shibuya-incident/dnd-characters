using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Entities;
using DndCharacters.Domain.Enum;

namespace DndCharacters.Infrastructure.Persistence.Shops
{
    public class ShopMockRepository : IShopRepository
    {
        private List<Shop> _shops =
            [
                new Shop
                {
                    Name = "The Rusty Sword",
                    ProfileImage = "https://example.com/images/rusty_sword.png",
                    ShopType = ShopType.Blacksmith,
                    OwnerName = "Gorim Ironfist",
                    Items =
                    [
                        new() { Name = "Rusty Sword", Price = 10 ,Id = 1,Description = "A rusty but functional sword." },
                        new() { Name = "Iron Dagger", Price = 5 ,Id = 2,Description = "A simple iron dagger." }
                    ]
                },
                new Shop
                {
                    Name = "Broken Soul",
                    ShopType = ShopType.MusicShop,
                    OwnerName = "Michi Mochievee",
                    Items =
                    [
                        new() { Name = "Quicksand", Price = 9999 ,Id = 1,Description = "A mysterious and powerful instrument techno heart broken song." },
                    ]
                }
            ];

        public IEnumerable<Shop> Get(GetShopsRequest request)
        {
            return _shops;
        }

    }
}
