using AutoMapper;

namespace SampleApiWebApp.Controllers.Teams
{
    public sealed class TeamMappings : Profile
    {
        public TeamMappings()
        {
            CreateMap<Domain.Team, Team>();
        }
    }
}
