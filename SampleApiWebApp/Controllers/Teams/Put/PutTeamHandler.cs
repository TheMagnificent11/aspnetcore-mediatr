using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntityManagement.Core;
using FluentValidation;
using FluentValidation.Results;
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

        protected override async Task BindToDomainEntityAndValidate(Domain.Team domainEntity, PutTeamRequest request, CancellationToken cancellationToken)
        {
            if (domainEntity == null) throw new ArgumentNullException(nameof(domainEntity));
            if (request == null) throw new ArgumentNullException(nameof(request));

            var query = new GetTeamsByName(request.Name);
            var teamsWithSameName = await this.Repository.Query(query, cancellationToken);

            if (teamsWithSameName.Any(i => i.Id != domainEntity.Id))
            {
                var error = new ValidationFailure(nameof(request.Name), string.Format(Domain.Team.ErrorMessages.NameNotUniqueFormat, request.Name));
                throw new ValidationException(new ValidationFailure[] { error });
            }

            domainEntity.ChangeName(request.Name);
        }
    }
}
