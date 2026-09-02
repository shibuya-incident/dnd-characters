using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Application.Dtos.Shops.AddShopItem;
using DndCharacters.Application.Dtos.Shops.CreateShop;
using DndCharacters.Application.Dtos.Shops.DeleteShop;
using DndCharacters.Application.Dtos.Shops.DeleteShopItem;
using DndCharacters.Application.Dtos.Shops.GetShopById;
using DndCharacters.Application.Dtos.Shops.GetShopItemById;
using DndCharacters.Application.Dtos.Shops.GetShopItems;
using DndCharacters.Application.Dtos.Shops.GetShops;
using DndCharacters.Application.Dtos.Shops.UpdateShop;
using DndCharacters.Application.Dtos.Shops.UpdateShopItem;
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
        [ProducesResponseType(typeof(PagedListResponse<GetShopsListItemResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetShops([FromQuery] GetShopsRequest request, CancellationToken cancellationToken = default)
        {
            PagedListResponse<GetShopsListItemResponse> response = await shopService.GetFilteredAsync(request, cancellationToken);
            return Ok(response);
        }

        // GET /shops/{id}
        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(GetShopByIdResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetShopById([FromRoute] int id)
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
            return CreatedAtAction(nameof(GetShopById), new { id = response.Id }, response);
        }

        // PUT /shops/{id}
        [HttpPut("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(UpdateShopResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateShop([FromRoute] int id, [FromBody] UpdateShopRequest request)
        {
            UpdateShopResponse response = await shopService.UpdateAsync(id, request);
            return Ok(response);
        }

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
        [HttpGet("{shopId}/items")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(GetShopItemsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetShopItems([FromRoute] int shopId)
        {
            GetShopItemsResponse response = await shopService.GetShopItemsAsync(new GetShopItemsRequest(shopId));
            return Ok(response);
        }

        // GET /shops/{id}/items/{itemId}
        [HttpGet("{shopId}/items/{itemId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(GetShopItemByIdResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetShopItemById([FromRoute] int shopId, [FromRoute] int itemId)
        {
            GetShopItemByIdResponse response = await shopService.GetShopItemByIdAsync(new GetShopItemByIdRequest(shopId, itemId));
            return Ok(response);
        }

        // POST /shops/{id}/items/{itemId}
        [HttpPost("{shopId}/items/{itemId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(AddShopItemResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddShopItem([FromRoute] int shopId, [FromRoute] int itemId, [FromBody] AddShopItemRequest request)
        {
            AddShopItemResponse response = await shopService.AddShopItemAsync(shopId, itemId, request);
            return CreatedAtAction(nameof(GetShopItemById), new { shopId = response.ShopId, itemId = response.ItemId }, response);
        }

        // PUT /shops/{id}/items/{itemId}
        [HttpPut("{shopId}/items/{itemId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(UpdateShopItemResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateShopItem([FromRoute] int shopId, [FromRoute] int itemId, [FromBody] UpdateShopItemRequest request)
        {
            UpdateShopItemResponse response = await shopService.UpdateShopItemAsync(shopId, itemId, request);
            return Ok(response);
        }

        // DELETE /shops/{id}/items/{itemId}
        [HttpDelete("{shopId}/items/{itemId}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteShopItem([FromRoute] int shopId, [FromRoute] int itemId)
        {
            await shopService.DeleteShopItemAsync(new DeleteShopItemRequest(shopId, itemId));
            return NoContent();
        }
    }
}
