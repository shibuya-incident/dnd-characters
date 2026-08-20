using FluentValidation;

namespace DndCharacters.Application.Dtos.Shops.CreateShop
{
    internal class CreateShopRequestValidator : AbstractValidator<CreateShopRequest>
    {
        public CreateShopRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.ShopType)
                .NotEmpty()
                .IsInEnum();

            RuleFor(x => x.OwnerName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.ProfileImage)
                .MaximumLength(500);

        }
    }
}
