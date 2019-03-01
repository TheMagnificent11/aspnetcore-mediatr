using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EntityManagement.Core;
using RequestManagement;
using SampleApiWebApp.Domain;

namespace SampleApiWebApp.Controllers.Players.Post
{
    public sealed class PostPlayerHandler : PostRequestHandler<long, Domain.Player, Player, PostPlayerRequest>
    {
        public PostPlayerHandler(IEntityRepository<Domain.Player, long> repository, IMapper mapper)
            : base(repository)
        {
            Mapper = mapper;
        }

        private IMapper Mapper { get; }

        protected override Task<Domain.Player> GenerateAndValidateDomainEntity(PostPlayerRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
