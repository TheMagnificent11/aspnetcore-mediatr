using System;
using AutoMapper;
using EntityManagement.Core;
using RequestManagement;

namespace SampleApiWebApp.Controllers.Players.Post
{
    public sealed class PostPlayerHandler :
        PostRequestHandler<long, Domain.Player, Player, PostPlayerRequest>
    {
        public PostPlayerHandler(IEntityRepository<Domain.Player, long> repository, IMapper mapper)
            : base(repository)
        {
            Mapper = mapper;
        }

        private IMapper Mapper { get; }

        protected override Domain.Player GenerateDomainEntity(PostPlayerRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
