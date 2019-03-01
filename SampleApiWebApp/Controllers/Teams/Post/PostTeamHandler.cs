using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntityManagement.Core;
using FluentValidation;
using FluentValidation.Results;
using RequestManagement;
using SampleApiWebApp.Data.Queries;

namespace SampleApiWebApp.Controllers.Teams.Post
{
    public sealed class PostTeamHandler : PostRequestHandler<long, Domain.Team, Team, PostTeamRequest>
    {
        public PostTeamHandler(IEntityRepository<Domain.Team, long> repository)
            : base(repository)
        {
        }

        protected override async Task<Domain.Team> GenerateAndValidateDomainEntity(PostTeamRequest request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var query = new GetTeamsByName(request.Name);
            var teamsWithSameName = await Repository.Query(query, cancellationToken);

            if (teamsWithSameName.Any())
            {
                var error = new ValidationFailure(nameof(request.Name), string.Format(Domain.Team.ErrorMessages.NameNotUniqueFormat, request.Name));
                throw new ValidationException(new ValidationFailure[] { error });
            }

            return Domain.Team.CreateTeam(request.Name);
        }
    }
}
