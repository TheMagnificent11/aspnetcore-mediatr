using System;
using AutoMapper;
using EntityManagement;
using RequestManagement;

namespace SampleApiWebApp.Handlers
{
    public sealed class PostTeamHandler : PostRequestHandler<long, Domain.Team, Models.Team, Models.Requests.PostTeamRequest>
    {
        public PostTeamHandler(IEntityRepository<Domain.Team, long> repository, IMapper mapper)
            : base(repository)
        {
            Mapper = mapper;
        }

        private IMapper Mapper { get; }

        protected override Domain.Team GenerateDomainEntity(Models.Requests.PostTeamRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return Mapper.Map<Domain.Team>(request);
        }
    }
}
