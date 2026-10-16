using FluentValidation;

namespace DndCharacters.Application.Dtos.Characters.UpdateCharacter
{
    internal class UpdateCharacterRequestValidator : AbstractValidator<UpdateCharacterRequest>
    {
        public UpdateCharacterRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Surname)
                .MaximumLength(100);

            RuleFor(x => x.Gender)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Height)
                .NotEmpty()
                .GreaterThan(0)
                .LessThan(1000);

            RuleFor(x => x.Birthday)
                .NotEmpty();

            RuleFor(x => x.Age)
                .NotEmpty()
                .GreaterThan(0)
                .LessThan(100000);

            RuleFor(x => x.Lore)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.DisplayImageUrl)
                .MaximumLength(500);

            RuleFor(x => x.Race)
                .NotEmpty()
                .IsInEnum();

            RuleFor(x => x.Class)
                .NotEmpty()
                .IsInEnum();
        }
    }
}
