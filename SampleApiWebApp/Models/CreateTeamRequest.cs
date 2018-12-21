using System.ComponentModel.DataAnnotations;
using RequestManagement;

namespace SampleApiWebApp.Models
{
    public class CreateTeamRequest : IPostRequest<long, Team>
    {
        [Required]
        [Display(Name = "Name")]
        [MaxLength(Domain.Team.NameMaxLength)]
        public string Name { get; set; }
    }
}
