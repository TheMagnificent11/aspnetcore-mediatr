using RequestManagement;

namespace SampleApiWebApp.Controllers.Teams.Post
{
    public class PostTeamRequest : IPostRequest<long, Team>
    {
        public string Name { get; set; }
    }
}
