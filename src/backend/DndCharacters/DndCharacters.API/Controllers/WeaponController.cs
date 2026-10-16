using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Application.Dtos.Weapons.CreateWeapon;
using DndCharacters.Application.Dtos.Weapons.DeleteWeapon;
using DndCharacters.Application.Dtos.Weapons.GetWeaponById;
using DndCharacters.Application.Dtos.Weapons.GetWeapons;
using DndCharacters.Application.Dtos.Weapons.GetWeaponsCount;
using DndCharacters.Application.Dtos.Weapons.UpdateWeapon;
using DndCharacters.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DndCharacters.API.Controllers
{
    [ApiController]
    [Route("api/weapons")]
    public class WeaponController(IWeaponService weaponService) : ControllerBase
    {
        // POST /weapons
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(CreateWeaponResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateWeapon([FromBody] CreateWeaponRequest request)
        {
            CreateWeaponResponse response = await weaponService.CreateAsync(request);
            return CreatedAtAction(nameof(GetWeaponById), new { id = response.Id }, response);
        }

        // GET /weapons/{id}
        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(GetWeaponByIdResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWeaponById([FromRoute] int id)
        {
            GetWeaponByIdResponse response = await weaponService.GetByIdAsync(new GetWeaponByIdRequest(id));
            return Ok(response);
        }

        // GET /weapons
        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(PagedListResponse<GetWeaponsListItemResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWeapons([FromQuery] GetWeaponsRequest request)
        {
            PagedListResponse<GetWeaponsListItemResponse> response = await weaponService.GetFilteredAsync(request);
            return Ok(response);
        }

        // PUT /weapons/{id}
        [HttpPut("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(UpdateWeaponResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateWeapon([FromRoute] int id, [FromBody] UpdateWeaponRequest request)
        {
            UpdateWeaponResponse response = await weaponService.UpdateAsync(id, request);
            return Ok(response);
        }

        // DELETE/weapons/{id}
        [HttpDelete("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteWeapon([FromRoute] int id)
        {
            await weaponService.DeleteAsync(new DeleteWeaponRequest(id));
            return NoContent();
        }

        //GET /weapons/count
        [HttpGet("count")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(GetWeaponsCountResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWeaponsCount()
        {
            GetWeaponsCountResponse response = await weaponService.GetWeaponsCountAsync();
            return Ok(response);
        }
    }
}
