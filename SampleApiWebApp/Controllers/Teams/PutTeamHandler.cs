using System;
using EntityManagement.Abstractions;
using RequestManagement;

namespace SampleApiWebApp.Controllers.Teams
{
    public sealed class PutTeamHandler : PutRequestHandler<long, Domain.Team, PutTeamRequest>
    {
        public PutTeamHandler(IEntityRepository<Domain.Team, long> repository)
            : base(repository)
        {
        }

        protected override Domain.Team BindToDomainEntity(Domain.Team domainEntity, PutTeamRequest request)
        {
            if (domainEntity == null) throw new ArgumentNullException(nameof(domainEntity));
            if (request == null) throw new ArgumentNullException(nameof(request));
        }
    }
}
