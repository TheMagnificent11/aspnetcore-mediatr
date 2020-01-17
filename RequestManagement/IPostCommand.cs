﻿using System;
using MediatR;

namespace RequestManagement
{
    /// <summary>
    /// Post Command Interface
    /// </summary>
    /// <typeparam name="TId">ID type of entity being created</typeparam>
    /// <typeparam name="TRequestEntity">Request entity type</typeparam>
    public interface IPostCommand<TId, TRequestEntity> : IRequest<CommandResult<TId>>
        where TId : IComparable, IComparable<TId>, IEquatable<TId>, IConvertible
        where TRequestEntity : class
    {
    }
}