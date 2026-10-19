using DndCharacters.Application.Dtos.Home.GetCounters;

namespace DndCharacters.Application.Interfaces
{
    public interface IHomeService
    {
        Task<GetHomeCountersResponse> GetCounters();
    }
}
