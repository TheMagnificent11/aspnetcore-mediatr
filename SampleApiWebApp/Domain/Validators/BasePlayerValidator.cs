using FluentValidation;

namespace SampleApiWebApp.Domain.Validators
{
    public abstract class BasePlayerValidator<T> : AbstractValidator<T>
        where T : IPlayer
    {
        protected BasePlayerValidator()
        {
            RuleFor(i => i.GivenName)
                .NotEmpty()
                .MaximumLength(Player.FieldNameMaxLengths.Name);

            RuleFor(i => i.Surname)
                .NotEmpty()
                .MaximumLength(Player.FieldNameMaxLengths.Name);

            RuleFor(i => i.TeamId)
                .GreaterThan(0);

            RuleFor(i => i.Number)
                .InclusiveBetween(1, 99);
        }
    }
}
