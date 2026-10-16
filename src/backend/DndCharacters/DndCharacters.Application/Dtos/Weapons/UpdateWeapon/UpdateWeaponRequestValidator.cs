using FluentValidation;

namespace DndCharacters.Application.Dtos.Weapons.UpdateWeapon
{
    internal class UpdateWeaponRequestValidator : AbstractValidator<UpdateWeaponRequest>
    {
        public UpdateWeaponRequestValidator()
        {

            RuleFor(x => x.Name)
               .NotEmpty()
               .MaximumLength(100);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.Weight)
                .NotEmpty()
                .GreaterThan(0)
                .LessThan(1000);

            RuleFor(x => x.Properties)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.WeaponType)
                .NotEmpty()
                .IsInEnum();

            RuleFor(x => x.DisplayImageUrl)
                .MaximumLength(500);

        }
    }
}
