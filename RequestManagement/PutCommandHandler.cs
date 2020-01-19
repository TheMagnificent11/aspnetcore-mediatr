using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using EntityManagement;
using EntityManagement.Core;
using FluentValidation;
using MediatR;
using RequestManagement.Logging;
using Serilog;
using Serilog.Context;

namespace RequestManagement
{
    /// <summary>
    /// Put Command Handler
    /// </summary>
    /// <typeparam name="TId">Database entity ID type</typeparam>
    /// <typeparam name="TEntity">Database entity type</typeparam>
    /// <typeparam name="TRequest">Put request type</typeparam>
    public abstract class PutCommandHandler<TId, TEntity, TRequest> :
        BaseRequestHandler<TId, TEntity>,
        IRequestHandler<TRequest, CommandResult>
        where TId : IComparable, IComparable<TId>, IEquatable<TId>, IConvertible
        where TEntity : class, IEntity<TId>
        where TRequest : class, IPutCommand<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PutCommandHandler{TId, TEntity, TRequest}"/> class
        /// </summary>
        /// <param name="repository">Entity repository</param>
        /// <param name="logger">Logger</param>
        protected PutCommandHandler(IEntityRepository<TEntity, TId> repository, ILogger logger)
            : base(repository, logger)
        {
        }

        /// <summary>
        /// Handles the put request
        /// </summary>
        /// <param name="request">Put request</param>
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

                try
                {
                    await this.BindToDomainEntityAndValidate(domainEntity, request, logger, cancellationToken);

                    await this.Repository.Update(domainEntity, cancellationToken);
                }
                catch (ValidationException ex)
                {
                    logger.Information(ex, "Validation failed");
                    return CommandResult.Fail(ex.Errors);
                }

                return CommandResult.Success();
            }
        }

        /// <summary>
        /// Generate a domain entity from create entity request
        /// </summary>
        /// <param name="domainEntity">Domain entity read from the database to be updated</param>
        /// <param name="request">Create entity request</param>
        /// <param name="logger">Logger</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Asynchronous task</returns>
        /// <exception cref="ValidationException">Exception thrown when validation errors occur</exception>
        protected abstract Task BindToDomainEntityAndValidate(
            [NotNull] TEntity domainEntity,
            [NotNull] TRequest request,
            [NotNull] ILogger logger,
            [NotNull] CancellationToken cancellationToken);
    }
}
