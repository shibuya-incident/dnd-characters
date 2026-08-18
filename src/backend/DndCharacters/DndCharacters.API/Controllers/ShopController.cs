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
        public IActionResult GetShops([FromQuery] GetShopsRequest request)
        {
            GetShopsResponse response = shopService.GetShops(request);
            return Ok(response);

        }


        // GET /shops/{id}
        // POST /shops
        // PUT /shops/{id}
        // DELETE/shops/{id}

        // GET /shops/{id}/items
        // GET /shops/{id}/items/{itemId}
        // POST /shops/{id}/items
        // PUT /shops/{id}/items
        // DELETE /shops/{id}/items
    }
}
