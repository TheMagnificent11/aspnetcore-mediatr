using System;
using System.Threading;
using System.Threading.Tasks;
using EntityManagement.Core;
using RequestManagement;

namespace SampleApiWebApp.Controllers.Players.Post
{
    public sealed class PostPlayerHandler : PostRequestHandler<long, Domain.Player, Player, PostPlayerRequest>
    {
        public PostPlayerHandler(IEntityRepository<Domain.Player, long> repository)
            : base(repository)
        {
        }

        protected override Task<Domain.Player> GenerateAndValidateDomainEntity(PostPlayerRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
