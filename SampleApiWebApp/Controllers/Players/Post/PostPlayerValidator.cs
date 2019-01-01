using SampleApiWebApp.Domain.Validators;

namespace SampleApiWebApp.Controllers.Players.Post
{
    public sealed class PostPlayerValidator : BasePlayerValidator<PostPlayerRequest>
    {
        public PostPlayerValidator()
            : base()
        {
        }
    }
}
