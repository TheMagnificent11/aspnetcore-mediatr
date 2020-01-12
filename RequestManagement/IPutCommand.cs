using System;
using MediatR;

namespace RequestManagement
{
    /// <summary>
    /// Put Command Interface
    /// </summary>
    /// <typeparam name="TId">Entity ID type</typeparam>
    public interface IPutCommand<TId> : IRequest<CommandResult>
        where TId : IComparable, IComparable<TId>, IEquatable<TId>, IConvertible
    {
        /// <summary>
        /// Gets or sets the entity ID
        /// </summary>
        TId Id { get; set; }
    }
}
