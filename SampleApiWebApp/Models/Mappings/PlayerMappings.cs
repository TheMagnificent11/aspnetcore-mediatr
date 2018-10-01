using AutoMapper;

namespace SampleApiWebApp.Models.Mappings
{
    /// <summary>
    /// Player Model Mappings
    /// </summary>
    public sealed class PlayerMappings : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerMappings"/> class
        /// </summary>
        public PlayerMappings()
        {
            CreateMap<Domain.Player, Player>();
            CreateMap<CreatePlayerRequest, Domain.Player>();
        }
    }
}
