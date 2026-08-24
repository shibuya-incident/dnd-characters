using DndCharacters.Application.Dtos.Items.CreateItem;
using DndCharacters.Application.Dtos.Items.DeleteItem;
using DndCharacters.Application.Dtos.Items.GetItemById;
using DndCharacters.Application.Dtos.Items.GetItems;
using DndCharacters.Application.Dtos.Items.UpdateItem;
using DndCharacters.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DndCharacters.API.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemController(IItemService itemService) : ControllerBase
    {
        // POST /items
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(CreateItemResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemRequest request)
        {
            CreateItemResponse response = await itemService.CreateAsync(request);
            return CreatedAtAction(nameof(GetItemById), new { id = response.Id }, response);
        }

        // GET /items/{id}
        [HttpGet("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(GetItemByIdResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetItemById([FromRoute] int id)
        {
            GetItemByIdResponse response = await itemService.GetByIdAsync(new GetItemByIdRequest(id));
            return Ok(response);
        }

        // GET /items
        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(GetItemsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetItems([FromQuery] GetItemsRequest request)
        {
            GetItemsResponse response = await itemService.GetFilteredAsync(request);
            return Ok(response);
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(UpdateItemResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateItem([FromRoute] int id, [FromBody] UpdateItemRequest request)
        {
            UpdateItemResponse response = await itemService.UpdateAsync(id, request);
            return Ok(response);
        }

        // DELETE/items/{id}
        [HttpDelete("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteItem([FromRoute] int id)
        {
            await itemService.DeleteAsync(new DeleteItemRequest(id));
            return NoContent();
        }

    }
}
