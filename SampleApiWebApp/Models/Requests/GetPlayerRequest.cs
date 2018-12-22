using RequestManagement;

namespace SampleApiWebApp.Models.Requests
{
    public class GetPlayerRequest : IGetOneRequest<long, Player>
    {
        public long Id { get; set; }
    }
}
