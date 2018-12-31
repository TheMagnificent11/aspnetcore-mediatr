using System.ComponentModel.DataAnnotations;
using RequestManagement;

namespace SampleApiWebApp.Controllers.Teams
{
    public sealed class PutTeamRequest : IPutRequest<long>
    {
        public long Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [MaxLength(Domain.Team.NameMaxLength)]
        public string Name { get; set; }
    }
}
