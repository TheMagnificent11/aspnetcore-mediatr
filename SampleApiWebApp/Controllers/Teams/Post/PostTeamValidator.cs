using FluentValidation;

namespace SampleApiWebApp.Controllers.Teams.Post
{
    public sealed class PostTeamValidator : AbstractValidator<PostTeamRequest>
    {
        public PostTeamValidator()
        {
            RuleFor(i => i.Name)
                .NotEmpty()
                .MaximumLength(Domain.Team.FieldMaxLenghts.Name);
        }
    }
}
