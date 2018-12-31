using RequestManagement;

namespace SampleApiWebApp.Controllers.Players.GetOne
{
    public class GetPlayerRequest : IGetOneRequest<long, Player>
    {
        public long Id { get; set; }
    }
}
