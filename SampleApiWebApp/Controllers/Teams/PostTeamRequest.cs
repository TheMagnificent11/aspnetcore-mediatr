using System.ComponentModel.DataAnnotations;
using RequestManagement;

namespace SampleApiWebApp.Controllers.Teams
{
    public class PostTeamRequest : IPostRequest<long, Team>
    {
        [Required]
        [Display(Name = "Name")]
        [MaxLength(Domain.Team.NameMaxLength)]
        public string Name { get; set; }
    }
}
