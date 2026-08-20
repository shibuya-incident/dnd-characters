using DndCharacters.Application.Dtos.Shops.CreateShop;
using DndCharacters.Application.Dtos.Shops.DeleteShop;
using DndCharacters.Application.Dtos.Shops.GetShopById;
using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DndCharacters.API.Controllers
{
    [ApiController]
    [Route("api/shops")]
    public class ShopController(IShopService shopService) : ControllerBase
    {

        // GET /shops
        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetShops([FromQuery] GetShopsRequest request)
        {
            GetShopsResponse response = await shopService.GetFilteredAsync(request);
            return Ok(response);

        }

        // GET /shops/{id}
        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            GetShopByIdResponse response = await shopService.GetByIdAsync(new GetShopByIdRequest(id));
            return Ok(response);
        }

        // POST /shops
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(CreateShopResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateShop([FromBody] CreateShopRequest request)
        {
            CreateShopResponse response = await shopService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        // PUT /shops/{id}


        // DELETE/shops/{id}
        [HttpDelete("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteShop([FromRoute] int id)
        {
            await shopService.DeleteAsync(new DeleteShopRequest(id));
            return NoContent();
        }

        // GET /shops/{id}/items
        // GET /shops/{id}/items/{itemId}
        // POST /shops/{id}/items
        // PUT /shops/{id}/items
        // DELETE /shops/{id}/items
    }
}
