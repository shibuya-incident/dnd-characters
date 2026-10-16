using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Characters.GetCharacters
{
    public class GetCharactersRequest : PageListRequest<GetCharactersSortByRequest>
    {
        public string? Name { get; set; }
        public CharacterRace? Race { get; set; }
        public CharacterClass? Class { get; set; }
    }
}
