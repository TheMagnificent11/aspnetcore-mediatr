using System.ComponentModel.DataAnnotations;
using RequestManagement;

namespace SampleApiWebApp.Models
{
    /// <summary>
    /// Create Player Request
    /// </summary>
    public class CreatePlayerRequest : IPostRequest<long>
    {
        /// <summary>
        /// Gets or sets the given name
        /// </summary>
        [Required]
        [Display(Name = "Given Name")]
        [MaxLength(Domain.Player.NameMaxLength)]
        public string GivenName { get; set; }

        /// <summary>
        /// Gets or sets the surname
        /// </summary>
        [Required]
        [Display(Name = "Surname")]
        [MaxLength(Domain.Player.NameMaxLength)]
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the team ID
        /// </summary>
        public long TeamId { get; set; }

        /// <summary>
        /// Gets or sets the squad number
        /// </summary>
        [Range(0, 99)]
        public int SquadNumber { get; set; }
    }
}
