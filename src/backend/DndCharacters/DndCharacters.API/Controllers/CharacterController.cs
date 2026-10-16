using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Application.Dtos.Characters.AddCharacterWeapon;
using DndCharacters.Application.Dtos.Characters.CreateCharacter;
using DndCharacters.Application.Dtos.Characters.DeleteCharacter;
using DndCharacters.Application.Dtos.Characters.DeleteCharacterWeapon;
using DndCharacters.Application.Dtos.Characters.GetCharacterById;
using DndCharacters.Application.Dtos.Characters.GetCharacters;
using DndCharacters.Application.Dtos.Characters.GetCharactersCount;
using DndCharacters.Application.Dtos.Characters.GetCharacterWeapons;
using DndCharacters.Application.Dtos.Characters.UpdateCharacter;
using DndCharacters.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DndCharacters.API.Controllers
{
    [ApiController]
    [Route("api/characters")]
    public class CharacterController(ICharacterService characterService) : ControllerBase
    {
        // GET /characters
        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(PagedListResponse<GetCharactersListItemResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCharacters([FromQuery] GetCharactersRequest request, CancellationToken cancellationToken = default)
        {
            PagedListResponse<GetCharactersListItemResponse> response = await characterService.GetFilteredAsync(request, cancellationToken);
            return Ok(response);
        }

        // GET /characters/{id}
        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(GetCharacterByIdResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCharacterById([FromRoute] int id)
        {
            GetCharacterByIdResponse response = await characterService.GetByIdAsync(new GetCharacterByIdRequest(id));
            return Ok(response);
        }

        // POST /characters
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(CreateCharacterResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCharacter([FromBody] CreateCharacterRequest request)
        {
            CreateCharacterResponse response = await characterService.CreateAsync(request);
            return CreatedAtAction(nameof(GetCharacterById), new { id = response.Id }, response);
        }

        // PUT /characters/{id}
        [HttpPut("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(UpdateCharacterResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCharacter([FromRoute] int id, [FromBody] UpdateCharacterRequest request)
        {
            UpdateCharacterResponse response = await characterService.UpdateAsync(id, request);
            return Ok(response);
        }

        // DELETE/characters/{id}
        [HttpDelete("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCharacter([FromRoute] int id)
        {
            await characterService.DeleteAsync(new DeleteCharacterRequest(id));
            return NoContent();
        }

        //GET /characters/count
        [HttpGet("count")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(GetCharactersCountResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCharactersCount()
        {
            GetCharactersCountResponse response = await characterService.GetCharactersCountAsync();
            return Ok(response);
        }

        //GET /characters/{id}/weapons
        [HttpGet("{characterId}/weapons")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(PagedListResponse<GetCharacterWeaponsListItemResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCharacterWeapons([FromRoute] int characterId, [FromQuery] GetCharacterWeaponsRequest request)
        {
            PagedListResponse<GetCharacterWeaponsListItemResponse> response = await characterService.GetCharacterWeaponsAsync(characterId, request);
            return Ok(response);
        }

        // POST /characters/{id}/weapons/{weaponId}
        [HttpPost("{characterId}/weapons/{weaponId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(AddCharacterWeaponResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddCharacterWeapon([FromRoute] int characterId, [FromRoute] int weaponId, [FromBody] AddCharacterWeaponRequest request)
        {
            AddCharacterWeaponResponse response = await characterService.AddCharacterWeaponAsync(characterId, weaponId, request);
            return Created();
        }

        // DELETE /characters/{id}/weapons/{weaponId}
        [HttpDelete("{characterId}/weapons/{weaponId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCharacterWeapon([FromRoute] int characterId, [FromRoute] int weaponId)
        {
            await characterService.DeleteCharacterWeaponAsync(new DeleteCharacterWeaponRequest(characterId, weaponId));
            return NoContent();
        }
    }
}
