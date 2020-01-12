using System.Collections.Generic;
using MediatR;

namespace RequestManagement
{
    /// <summary>
    /// Get All Entities Query Interface
    /// </summary>
    /// <typeparam name="T">Type of entity retrieved</typeparam>
    public interface IGetAllQuery<T> : IRequest<CommandResult<IEnumerable<T>>>
    {
    }
}
