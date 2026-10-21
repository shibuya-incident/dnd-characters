namespace DndCharacters.Application.Dtos.Home.GetCounters
{
    public record GetHomeCountersResponse(
        int CharactersCount,
        int WeaponsCount,
        int ItemsCount,
        int ShopsCount);
}
