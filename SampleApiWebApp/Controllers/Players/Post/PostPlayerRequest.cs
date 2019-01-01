using RequestManagement;
using SampleApiWebApp.Domain;

namespace SampleApiWebApp.Controllers.Players.Post
{
    public class PostPlayerRequest : IPostRequest<long, Player>, IPlayer
    {
        public string GivenName { get; set; }

        public string Surname { get; set; }

        public long TeamId { get; set; }

        public int Number { get; set; }
    }
}
