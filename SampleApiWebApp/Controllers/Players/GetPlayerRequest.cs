using RequestManagement;

namespace SampleApiWebApp.Controllers.Players
{
    public class GetPlayerRequest : IGetOneRequest<long, Player>
    {
        public long Id { get; set; }
    }
}
