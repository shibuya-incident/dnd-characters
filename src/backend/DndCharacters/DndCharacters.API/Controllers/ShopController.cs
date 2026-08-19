using DndCharacters.Application.Dtos.Shops.CreateShop;
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
        public IActionResult GetShops([FromQuery] GetShopsRequest request)
        {
            GetShopsResponse response = shopService.GetFiltered(request);
            return Ok(response);

        }

        // GET /shops/{id}
        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetById([FromRoute] int id)
        {
            GetShopByIdResponse response = shopService.GetById(new GetShopByIdRequest(id));
            return Ok(response);
        }

        // POST /shops
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(CreateShopResponse), StatusCodes.Status201Created)]
        public IActionResult CreateShop([FromBody] CreateShopRequest request)
        {
            CreateShopResponse response = shopService.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        // PUT /shops/{id}
        // DELETE/shops/{id}

        // GET /shops/{id}/items
        // GET /shops/{id}/items/{itemId}
        // POST /shops/{id}/items
        // PUT /shops/{id}/items
        // DELETE /shops/{id}/items
    }
}
