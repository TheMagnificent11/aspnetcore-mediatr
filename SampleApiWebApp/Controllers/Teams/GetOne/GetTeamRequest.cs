using RequestManagement;

namespace SampleApiWebApp.Controllers.Teams.GetOne
{
    public class GetTeamRequest : IGetOneRequest<long, Team>
    {
        public long Id { get; set; }
    }
}
