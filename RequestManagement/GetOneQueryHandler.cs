using System;
using System.Threading;
using System.Threading.Tasks;
using EntityManagement;
using EntityManagement.Core;
using MediatR;
using RequestManagement.Logging;
using Serilog;
using Serilog.Context;

namespace RequestManagement
{
    /// <summary>
    /// Get One Query Handler
    /// </summary>
    /// <typeparam name="TId">Database entity ID type</typeparam>
    /// <typeparam name="TEntity">Database entity type</typeparam>
    /// <typeparam name="TResponseEntity">Entity response type</typeparam>
    /// <typeparam name="TRequest">Request type</typeparam>
    public abstract class GetOneQueryHandler<TId, TEntity, TResponseEntity, TRequest> :
        BaseRequestHandler<TId, TEntity>,
        IRequestHandler<TRequest, CommandResult<TResponseEntity>>
        where TId : IComparable, IComparable<TId>, IEquatable<TId>, IConvertible
        where TEntity : class, IEntity<TId>
        where TResponseEntity : class
        where TRequest : class, IGetOneQuery<TId, TResponseEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetOneQueryHandler{TId, TEntity, TResponseEntity, TRequest}"/> class
        /// </summary>
        /// <param name="repository">Entity repository</param>
        /// <param name="logger">Logger</param>
        protected GetOneQueryHandler(IEntityRepository<TEntity, TId> repository, ILogger logger)
            : base(repository, logger)
        {
        }

        /// <summary>
        /// Handles get one entity requests
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Operation result containing the response entity as the data if successful,
        /// otherwise not found operation result
        /// </returns>
        public async Task<CommandResult<TResponseEntity>> Handle(
            TRequest request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var logger = this.GetLoggerForContext();

            using (LogContext.PushProperty(LoggingProperties.EntityType, typeof(TEntity).Name))
            using (LogContext.PushProperty(LoggingProperties.EntityId, request.Id))
            using (logger.BeginTimedOperation(this.GetLoggerTimedOperationName()))
            {
                var entity = await this.Repository.RetrieveById(request.Id, cancellationToken);
                if (entity == null) return CommandResult.NotFound<TResponseEntity>();

                var result = this.MapEntity(entity);

                return CommandResult.Success(result);
            }
        }

        /// <summary>
        /// Maps a domain entity to a response entity
        /// </summary>
        /// <param name="entity">Domain entity</param>
        /// <returns>Response entity</returns>
        protected abstract TResponseEntity MapEntity(TEntity entity);
    }
}
