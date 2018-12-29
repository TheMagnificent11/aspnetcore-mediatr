using System;
using EntityManagement;
using RequestManagement;
using SampleApiWebApp.Domain;
using SampleApiWebApp.Models.Requests;

namespace SampleApiWebApp.Handlers
{
    public sealed class PutTeamHandler : PutRequestHandler<long, Team, PutTeamRequest>
    {
        public PutTeamHandler(IEntityRepository<Team, long> repository)
            : base(repository)
        {
        }

        protected override Team BindToDomainEntity(Team domainEntity, PutTeamRequest request)
        {
            if (domainEntity == null) throw new ArgumentNullException(nameof(domainEntity));
            if (request == null) throw new ArgumentNullException(nameof(request));

            domainEntity.Name = request.Name;

            return domainEntity;
        }
    }
}
