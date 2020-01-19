using System;
using EntityManagement;
using EntityManagement.Core;
using Serilog;

namespace RequestManagement
{
    /// <summary>
    /// Base Request Handler
    /// </summary>
    /// <typeparam name="TId">Database entity ID type</typeparam>
    /// <typeparam name="TEntity">Database entity type</typeparam>
    public abstract class BaseRequestHandler<TId, TEntity>
        where TId : IComparable, IComparable<TId>, IEquatable<TId>, IConvertible
        where TEntity : class, IEntity<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRequestHandler{TId, TEntity}"/> class
        /// </summary>
        /// <param name="repository">Entity repository</param>
        /// <param name="logger">Logger</param>
        protected BaseRequestHandler(IEntityRepository<TEntity, TId> repository, ILogger logger)
        {
            this.Repository = repository;
            this.Logger = logger;
            this.HandlerType = this.GetType();
        }

        /// <summary>
        /// Gets the entity repository
        /// </summary>
        protected IEntityRepository<TEntity, TId> Repository { get; }

        private ILogger Logger { get; }

        private Type HandlerType { get; }

        internal string GetLoggerTimedOperationName()
        {
            return $"{this.HandlerType.Name}.Handle";
        }

        /// <summary>
        /// Gets a logger in the context of this handler
        /// </summary>
        /// <returns>A logger</returns>
        protected ILogger GetLoggerForContext()
        {
            return this.Logger.ForContext(this.HandlerType);
        }
    }
}
