using System;
using System.Threading;
using System.Threading.Tasks;
using EntityManagement.Core;
using RequestManagement;

namespace SampleApiWebApp.Controllers.Players.Post
{
    public sealed class PostPlayerHandler : PostCommandHandler<long, Domain.Player, Player, PostPlayerCommand>
    {
        public PostPlayerHandler(IEntityRepository<Domain.Player, long> repository)
            : base(repository)
        {
        }

        protected override Task<Domain.Player> GenerateAndValidateDomainEntity(PostPlayerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
