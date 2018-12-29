using System;
using AutoMapper;
using EntityManagement;
using RequestManagement;

namespace SampleApiWebApp.Handlers
{
    public sealed class PostPlayerHandler :
        PostRequestHandler<long, Domain.Player, Models.Player, Models.Requests.PostPlayerRequest>
    {
        public PostPlayerHandler(IEntityRepository<Domain.Player, long> repository, IMapper mapper)
            : base(repository)
        {
            Mapper = mapper;
        }

        private IMapper Mapper { get; }

        protected override Domain.Player GenerateDomainEntity(Models.Requests.PostPlayerRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return Mapper.Map<Domain.Player>(request);
        }
    }
}
