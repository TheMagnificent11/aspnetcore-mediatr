using RequestManagement;
using SampleApiWebApp.Domain;

namespace SampleApiWebApp.Controllers.Teams.Post
{
    public class PostTeamRequest : IPostRequest<long, Team>, ITeam
    {
        public string Name { get; set; }
    }
}
