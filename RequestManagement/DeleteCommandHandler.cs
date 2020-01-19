using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Delete Command Handler
    /// </summary>
    /// <typeparam name="TId">Database entity ID type</typeparam>
    /// <typeparam name="TEntity">Database entity type</typeparam>
    /// <typeparam name="TRequest">Delete command type</typeparam>
    public abstract class DeleteCommandHandler<TId, TEntity, TRequest> :
        BaseRequestHandler<TId, TEntity>,
        IRequestHandler<TRequest, CommandResult>
        where TId : IComparable, IComparable<TId>, IEquatable<TId>, IConvertible
        where TEntity : class, IEntity<TId>
        where TRequest : class, IDeleteCommand<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommandHandler{TId, TEntity, TRequest}"/> class
        /// </summary>
        /// <param name="repository">Entity repository</param>
        /// <param name="logger">Logger</param>
        protected DeleteCommandHandler(IEntityRepository<TEntity, TId> repository, ILogger logger)
            : base(repository, logger)
        {
        }

        /// <summary>
        /// Handles the delete request
        /// </summary>
        /// <param name="request">Delete request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>An <see cref="CommandResult"/> that reports success and any validation errors it was a bad request</returns>
        public async Task<CommandResult> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var logger = this.GetLoggerForContext();

            using (LogContext.PushProperty(LoggingProperties.EntityType, typeof(TEntity).Name))
            using (LogContext.PushProperty(LoggingProperties.EntityId, request.Id))
            using (logger.BeginTimedOperation(this.GetLoggerTimedOperationName()))
            {
                var domainEntity = await this.Repository.RetrieveById(request.Id, cancellationToken);
                if (domainEntity == null) return CommandResult.NotFound();

                var validationErrors = await this.ValidateDeletion(domainEntity, request, logger, cancellationToken);
                if (validationErrors != null && validationErrors.Any()) return CommandResult.Fail(validationErrors);

                await this.Repository.Delete(domainEntity.Id, cancellationToken);

                return CommandResult.Success();
            }
        }

        /// <summary>
        /// Validates whether deletion is allowed
        /// </summary>
        /// <param name="domainEntity">Entity to delete</param>
        /// <param name="request">Delete request</param>
        /// <param name="logger">The logger</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Dictionary of validation errors keyed by request field name</returns>
        protected virtual Task<IDictionary<string, IEnumerable<string>>> ValidateDeletion(
            TEntity domainEntity,
            TRequest request,
            ILogger logger,
            CancellationToken cancellationToken)
        {
            IDictionary<string, IEnumerable<string>> validationErrors = new Dictionary<string, IEnumerable<string>>();
            return Task.FromResult(validationErrors);
        }
    }
}
