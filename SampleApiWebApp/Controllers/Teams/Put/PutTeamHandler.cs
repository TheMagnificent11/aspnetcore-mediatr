using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntityManagement.Abstractions;
using RequestManagement;
using SampleApiWebApp.Data.Queries;

namespace SampleApiWebApp.Controllers.Teams.Put
{
    public sealed class PutTeamHandler : PutRequestHandler<long, Domain.Team, PutTeamRequest>
    {
        public PutTeamHandler(IEntityRepository<Domain.Team, long> repository)
            : base(repository)
        {
        }

        protected override async Task<IDictionary<string, IEnumerable<string>>> ValidateRequest(
            Domain.Team domainEntity,
            PutTeamRequest request,
            CancellationToken cancellationToken)
        {
            if (domainEntity == null) throw new ArgumentNullException(nameof(request));
            if (request == null) throw new ArgumentNullException(nameof(request));

            var errors = new Dictionary<string, IEnumerable<string>>();

            var query = new GetTeamsByName(request.Name);
            var teamsWithSameName = await Repository.Query(query, cancellationToken);

            if (teamsWithSameName.Any(i => i.Id != domainEntity.Id))
            {
                errors.Add(
                    nameof(request.Name),
                    new string[] { string.Format(Domain.Team.ErrorMessages.NameNotUniqueFormat, request.Name) });
            }

            return errors;
        }

        protected override void BindToDomainEntity(Domain.Team domainEntity, PutTeamRequest request)
        {
            if (domainEntity == null) throw new ArgumentNullException(nameof(domainEntity));
            if (request == null) throw new ArgumentNullException(nameof(request));

            domainEntity.ChangeName(domainEntity, request.Name);
        }
    }
}
