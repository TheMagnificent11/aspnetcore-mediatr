using RequestManagement;

namespace SampleApiWebApp.Models
{
    public class GetTeamRequest : IGetOneRequest<long, Team>
    {
        public long Id { get; set; }
    }
}
