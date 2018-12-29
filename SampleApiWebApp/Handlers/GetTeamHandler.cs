using System;
using AutoMapper;
using EntityManagement;
using RequestManagement;

namespace SampleApiWebApp.Handlers
{
    public sealed class GetTeamHandler : GetOneHandler<long, Domain.Team, Models.Team, Models.Requests.GetTeamRequest>
    {
        public GetTeamHandler(IEntityRepository<Domain.Team, long> repository, IMapper mapper)
            : base(repository)
        {
            Mapper = mapper;
        }

        private IMapper Mapper { get; }

        protected override Models.Team MapEntity(Domain.Team entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return Mapper.Map<Models.Team>(entity);
        }
    }
}
