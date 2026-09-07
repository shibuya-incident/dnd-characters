using DndCharacters.Application.Commons.Pagination;
using DndCharacters.Domain.Enum;

namespace DndCharacters.Application.Dtos.Items.GetItems
{
    public class GetItemsRequest : PageListRequest<GetItemsSortByRequest>
    {
        public string? Name { get; set; }
        public ItemType? ItemType { get; set; }
    }


}
