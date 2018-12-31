using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EntityManagement.Abstractions;
using RequestManagement;
using SampleApiWebApp.Data.Queries;

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

        protected override async Task<IDictionary<string, IEnumerable<string>>> ValidateRequest(
            PostTeamRequest request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var errors = new Dictionary<string, IEnumerable<string>>();

            var query = new GetTeamsByName(request.Name);
            var teamsWithSameName = await Repository.Query(query, cancellationToken);

            if (teamsWithSameName.Any())
            {
                errors.Add(nameof(request.Name), new string[] { "Team name is not unique" });
            }

            return errors;
        }

        protected override Domain.Team GenerateDomainEntity(PostTeamRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            return Domain.Team.CreateTeam(request.Name);
        }
    }
}
