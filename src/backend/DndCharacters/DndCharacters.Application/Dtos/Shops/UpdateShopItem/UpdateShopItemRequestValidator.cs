using FluentValidation;

namespace DndCharacters.Application.Dtos.Shops.UpdateShopItem
{
    internal class UpdateShopItemRequestValidator : AbstractValidator<UpdateShopItemRequest>
    {
        public UpdateShopItemRequestValidator()
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
