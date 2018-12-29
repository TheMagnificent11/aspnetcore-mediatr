using System;
using MediatR;

namespace RequestManagement
{
    /// <summary>
    /// Get One Entity Request Interface
    /// </summary>
    /// <typeparam name="TId">Entity ID type</typeparam>
    /// <typeparam name="TResponseEntity">Reponse entity type</typeparam>
    public interface IGetOneRequest<TId, TResponseEntity> : IRequest<OperationResult<TResponseEntity>>
        where TId : IComparable, IComparable<TId>, IEquatable<TId>, IConvertible
        where TResponseEntity : class
    {
        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        TId Id { get; set; }
    }
}
