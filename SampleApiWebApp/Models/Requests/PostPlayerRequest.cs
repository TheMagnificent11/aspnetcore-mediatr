using System.ComponentModel.DataAnnotations;
using RequestManagement;

namespace SampleApiWebApp.Models.Requests
{
    public class PostPlayerRequest : IPostRequest<long, Player>
    {
        [Required]
        [Display(Name = "Given Name")]
        [MaxLength(Domain.Player.NameMaxLength)]
        public string GivenName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        [MaxLength(Domain.Player.NameMaxLength)]
        public string Surname { get; set; }

        public long TeamId { get; set; }

        [Range(0, 99)]
        public int SquadNumber { get; set; }
    }
}
