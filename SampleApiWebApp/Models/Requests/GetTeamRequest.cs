using RequestManagement;

namespace SampleApiWebApp.Models.Requests
{
    public class GetTeamRequest : IGetOneRequest<long, Team>
    {
        public long Id { get; set; }
    }
}
