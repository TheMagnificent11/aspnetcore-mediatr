using System;
using AutoMapper;
using EntityManagement.Core;
using RequestManagement;

namespace SampleApiWebApp.Controllers.Teams.GetOne
{
    public sealed class GetTeamHandler : GetOneHandler<long, Domain.Team, Team, GetTeamRequest>
    {
        public GetTeamHandler(IEntityRepository<Domain.Team, long> repository, IMapper mapper)
            : base(repository)
        {
            Mapper = mapper;
        }

        private IMapper Mapper { get; }

        protected override Team MapEntity(Domain.Team entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return Mapper.Map<Team>(entity);
        }
    }
}
