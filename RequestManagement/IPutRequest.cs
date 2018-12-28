using MediatR;

namespace RequestManagement
{
    /// <summary>
    /// Put Request Interface
    /// </summary>
    /// <typeparam name="TId">Entity ID type</typeparam>
    public interface IPutRequest<TId> : IRequest<OperationResult>
    {
        /// <summary>
        /// Gets or sets the entity ID
        /// </summary>
        TId Id { get; set; }
    }
}
