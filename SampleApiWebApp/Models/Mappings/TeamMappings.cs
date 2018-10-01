﻿using AutoMapper;

namespace SampleApiWebApp.Models.Mappings
{
    /// <summary>
    /// Team Mappings
    /// </summary>
    public sealed class TeamMappings : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeamMappings"/> class
        /// </summary>
        public TeamMappings()
        {
            CreateMap<Domain.Team, Team>();
            CreateMap<CreateTeamRequest, Domain.Team>();
        }
    }
}