using DndCharacters.Application.Dtos.Home.GetCounters;
using DndCharacters.Application.Interfaces;

namespace DndCharacters.Application.Services
{
    public class HomeService(
        IShopRepository shopRepository,
        IWeaponRepository weaponRepository,
        IItemRepository itemRepository,
        ICharacterRepository characterRepository) : IHomeService
    {
        public async Task<GetHomeCountersResponse> GetCounters()
        {
            var charactersCount = await characterRepository.CountAsync();
            var weaponsCount = await weaponRepository.CountAsync();
            var itemsCount = await itemRepository.CountAsync();
            var shopsCount = await shopRepository.CountAsync();

            return new GetHomeCountersResponse
            (
               charactersCount,
               weaponsCount,
               itemsCount,
               shopsCount
            );
        }
    }
}
