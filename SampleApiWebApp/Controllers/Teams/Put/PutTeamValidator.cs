using FluentValidation;

namespace SampleApiWebApp.Controllers.Teams.Put
{
    public sealed class PutTeamValidator : AbstractValidator<PutTeamRequest>
    {
        public PutTeamValidator()
        {
            RuleFor(i => i.Name)
                .NotEmpty()
                .MaximumLength(Domain.Team.FieldMaxLenghts.Name);
        }
    }
}
