using System;
using AutoMapper;
using EntityManagement.Abstractions;
using RequestManagement;

namespace SampleApiWebApp.Controllers.Teams
{
    public sealed class PostTeamHandler : PostRequestHandler<long, Domain.Team, Team, PostTeamRequest>
    {
        public PostTeamHandler(IEntityRepository<Domain.Team, long> repository, IMapper mapper)
            : base(repository)
        {
            Mapper = mapper;
        }

        private IMapper Mapper { get; }

        protected override Domain.Team GenerateDomainEntity(PostTeamRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return Mapper.Map<Domain.Team>(request);
        }
    }
}
