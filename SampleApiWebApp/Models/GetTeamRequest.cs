using RequestManagement;

namespace SampleApiWebApp.Models
{
    /// <summary>
    /// Get Team Request
    /// </summary>
    public class GetTeamRequest : IGetOneRequest<long, Team>
    {
        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        public long Id { get; set; }
    }
}
