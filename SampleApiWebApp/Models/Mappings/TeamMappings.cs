using AutoMapper;

namespace SampleApiWebApp.Models.Mappings
{
    public sealed class TeamMappings : Profile
    {
        public TeamMappings()
        {
            CreateMap<Domain.Team, Team>();
            CreateMap<CreateTeamRequest, Domain.Team>();
        }
    }
}
