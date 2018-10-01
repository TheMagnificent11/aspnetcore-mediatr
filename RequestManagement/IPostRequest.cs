using System;
using MediatR;

namespace RequestManagement
{
    /// <summary>
    /// Post Request Interface
    /// </summary>
    /// <typeparam name="TId">ID type of entity being created</typeparam>
    public interface IPostRequest<TId> : IRequest<OperationResult<TId>>
        where TId : IComparable, IComparable<TId>, IEquatable<TId>, IConvertible
    {
    }
}
