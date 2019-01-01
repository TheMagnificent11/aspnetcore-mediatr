using FluentValidation;

namespace SampleApiWebApp.Domain.Validators
{
    public abstract class BaseTeamValidator<T> : AbstractValidator<T>
        where T : ITeam
    {
        protected BaseTeamValidator()
        {
            RuleFor(i => i.Name)
                .NotEmpty()
                .MaximumLength(Team.FieldMaxLenghts.Name);
        }
    }
}
