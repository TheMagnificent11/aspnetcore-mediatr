using System;
using MediatR;

namespace RequestManagement
{
    /// <summary>
    /// Put Request Interface
    /// </summary>
    /// <typeparam name="TId">Entity ID type</typeparam>
    public interface IPutRequest<TId> : IRequest<OperationResult>
        where TId : IComparable, IComparable<TId>, IEquatable<TId>, IConvertible
    {
        /// <summary>
        /// Gets or sets the entity ID
        /// </summary>
        TId Id { get; set; }
    }
}
