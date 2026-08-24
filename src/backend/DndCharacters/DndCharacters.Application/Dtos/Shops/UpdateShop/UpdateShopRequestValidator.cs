using FluentValidation;

namespace DndCharacters.Application.Dtos.Shops.UpdateShop
{
    internal class UpdateShopRequestValidator : AbstractValidator<UpdateShopRequest>
    {

        public UpdateShopRequestValidator()
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
