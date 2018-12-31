using RequestManagement;

namespace SampleApiWebApp.Controllers.Teams
{
    public class GetTeamRequest : IGetOneRequest<long, Team>
    {
        public long Id { get; set; }
    }
}
