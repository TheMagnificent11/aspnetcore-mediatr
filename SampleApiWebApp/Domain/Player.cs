﻿using EntityManagement;

namespace SampleApiWebApp.Domain
{
    /// <summary>
    /// Player
    /// </summary>
    public class Player : IEntity<long>
    {
        /// <summary>
        /// Name max length
        /// </summary>
        public const int NameMaxLength = 50;

        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the given name
        /// </summary>
        public string GivenName { get; set; }

        /// <summary>
        /// Gets or sets the surname
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the team ID
        /// </summary>
        public long TeamId { get; set; }

        /// <summary>
        /// Gets or sets the team
        /// </summary>
        public Team Team { get; set; }

        /// <summary>
        /// Gets or sets the squad number
        /// </summary>
        public int SquadNumber { get; set; }
    }
}