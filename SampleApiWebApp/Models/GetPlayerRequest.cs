using RequestManagement;

namespace SampleApiWebApp.Models
{
    public class GetPlayerRequest : IGetOneRequest<long, Player>
    {
        public long Id { get; set; }
    }
}
