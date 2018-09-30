using System.Collections.Generic;
using EntityManagement;

namespace SampleApiWebApp.Domain
{
    /// <summary>
    /// Team
    /// </summary>
    public class Team : IEntity<long>
    {
        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the players
        /// </summary>
        public IList<Player> Players { get; set; }
    }
}
