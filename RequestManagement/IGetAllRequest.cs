using System.Collections.Generic;
using MediatR;

namespace RequestManagement
{
    /// <summary>
    /// Get All Request Interface
    /// </summary>
    /// <typeparam name="T">Type of entity retrieved</typeparam>
    public interface IGetAllRequest<T> : IRequest<OperationResult<IEnumerable<T>>>
    {
    }
}
