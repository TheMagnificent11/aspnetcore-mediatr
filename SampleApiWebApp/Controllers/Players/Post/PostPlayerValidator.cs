using FluentValidation;

namespace SampleApiWebApp.Controllers.Players.Post
{
    public sealed class PostPlayerValidator : AbstractValidator<PostPlayerRequest>
    {
        public PostPlayerValidator()
        {
            RuleFor(i => i.GivenName)
                .NotEmpty()
                .MaximumLength(Domain.Player.FieldNameMaxLengths.Name);

            RuleFor(i => i.Surname)
                .NotEmpty()
                .MaximumLength(Domain.Player.FieldNameMaxLengths.Name);

            RuleFor(i => i.SquadNumber)
                .InclusiveBetween(1, 99);
        }
    }
}
