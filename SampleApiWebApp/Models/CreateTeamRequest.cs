using System.ComponentModel.DataAnnotations;
using RequestManagement;

namespace SampleApiWebApp.Models
{
    /// <summary>
    /// Create Team Request
    /// </summary>
    public class CreateTeamRequest : IPostRequest<long>
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [Required]
        [Display(Name = "Name")]
        [MaxLength(0)]
        public string Name { get; set; }
    }
}
