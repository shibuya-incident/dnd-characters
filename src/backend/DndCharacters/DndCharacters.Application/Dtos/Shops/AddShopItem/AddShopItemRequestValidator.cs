using FluentValidation;

namespace DndCharacters.Application.Dtos.Shops.AddShopItem
{
    internal class AddShopItemRequestValidator : AbstractValidator<AddShopItemRequest>
    {
        public AddShopItemRequestValidator()
        {

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThan(0)
                .LessThan(1000000);

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0)
                .LessThan(999);

        }
    }
}
