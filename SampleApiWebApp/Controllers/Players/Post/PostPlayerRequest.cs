using System.ComponentModel.DataAnnotations;
using RequestManagement;

namespace SampleApiWebApp.Controllers.Players.Post
{
    public class PostPlayerRequest : IPostRequest<long, Player>
    {
        public string GivenName { get; set; }

        public string Surname { get; set; }

        public long TeamId { get; set; }

        [Range(0, 99)]
        public int SquadNumber { get; set; }
    }
}
