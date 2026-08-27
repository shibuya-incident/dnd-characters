using DndCharacters.Application.Dtos.Items.CreateItem;
using DndCharacters.Application.Dtos.Items.DeleteItem;
using DndCharacters.Application.Dtos.Items.GetItemById;
using DndCharacters.Application.Dtos.Items.GetItems;
using DndCharacters.Application.Dtos.Items.UpdateItem;
using DndCharacters.Application.Interfaces;
using DndCharacters.Domain.Entities;
using FluentValidation;

namespace DndCharacters.Application.Services
{
    public class ItemService(IItemRepository itemRepository) : IItemService
    {
        public async Task<CreateItemResponse> CreateAsync(CreateItemRequest request)
        {
            await new CreateItemRequestValidator().ValidateAndThrowAsync(request);

            Item item = Item.Create(
                request.Name,
                request.Description,
                request.ItemType,
                request.DisplayImageUrl);

            await itemRepository.AddAsync(item);

            return new CreateItemResponse(
                item.Id,
                item.Name,
                item.Description,
                item.ItemType,
                item.DisplayImageUrl);
        }

        public async Task DeleteAsync(DeleteItemRequest request)
        {
            Item item = await itemRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Item with id {request.Id} not found.");

            await itemRepository.Remove(item);
        }

        public async Task<GetItemByIdResponse> GetByIdAsync(GetItemByIdRequest request)
        {
            Item item = await itemRepository.GetByIdAsync(request.Id)
                ?? throw new KeyNotFoundException($"Item with id {request.Id} not found.");

            return new GetItemByIdResponse(
                item.Id,
                item.Name,
                item.Description,
                item.ItemType,
                item.DisplayImageUrl);
        }

        public async Task<GetItemsResponse> GetFilteredAsync(GetItemsRequest request)
        {
            return await itemRepository.GetAsync(request);
        }

        public async Task<UpdateItemResponse> UpdateAsync(int id, UpdateItemRequest request)
        {

            await new UpdateItemRequestValidator().ValidateAndThrowAsync(request);

            Item item = await itemRepository.GetByIdAsync(id)
                ?? throw new Exception($"Item with id {id} not found.");

            item.Name = request.Name;
            item.Description = request.Description;
            item.ItemType = request.ItemType;
            item.DisplayImageUrl = request.DisplayImageUrl;

            await itemRepository.UpdateAsync(item);

            return new UpdateItemResponse(
                item.Id,
                item.Description,
                item.Name,
                item.ItemType,
                item.DisplayImageUrl);
        }
    }
}
