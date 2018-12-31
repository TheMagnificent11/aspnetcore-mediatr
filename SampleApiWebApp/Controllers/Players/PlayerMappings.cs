using AutoMapper;

namespace SampleApiWebApp.Controllers.Players
{
    public sealed class PlayerMappings : Profile
    {
        public PlayerMappings()
        {
            CreateMap<Domain.Player, Player>();
        }
    }
}
