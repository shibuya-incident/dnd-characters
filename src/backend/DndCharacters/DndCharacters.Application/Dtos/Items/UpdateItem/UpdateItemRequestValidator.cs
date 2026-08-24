using FluentValidation;

namespace DndCharacters.Application.Dtos.Items.UpdateItem
{
    internal class UpdateItemRequestValidator : AbstractValidator<UpdateItemRequest>
    {
        public UpdateItemRequestValidator()
        {

            RuleFor(x => x.Name)
               .NotEmpty()
               .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.ItemType)
                .NotEmpty()
                .IsInEnum();

            RuleFor(x => x.DisplayImageUrl)
                .MaximumLength(500);

        }
    }
}
