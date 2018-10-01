using RequestManagement;

namespace SampleApiWebApp.Models
{
    /// <summary>
    /// Get Player Request
    /// </summary>
    public class GetPlayerRequest : IGetOneRequest<long, Player>
    {
        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        public long Id { get; set; }
    }
}
