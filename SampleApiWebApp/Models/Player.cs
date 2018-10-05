namespace SampleApiWebApp.Models
{
    /// <summary>
    /// Player
    /// </summary>
    public class Player
    {
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
        /// Gets or sets the squad number
        /// </summary>
        public int SquadNumber { get; set; }
    }
}
