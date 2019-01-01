using RequestManagement;

namespace SampleApiWebApp.Controllers.Teams.Put
{
    public sealed class PutTeamRequest : IPutRequest<long>
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
