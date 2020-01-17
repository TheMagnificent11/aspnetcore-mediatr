using System;
using System.Threading;
using System.Threading.Tasks;
using EntityManagement;
using EntityManagement.Core;
using FluentValidation;
using MediatR;

namespace RequestManagement
{
    /// <summary>
    /// Put Command Handler
    /// </summary>
    /// <typeparam name="TId">Database entity ID type</typeparam>
    /// <typeparam name="TEntity">Database entity type</typeparam>
    /// <typeparam name="TRequest">Put request type</typeparam>
    public abstract class PutCommandHandler<TId, TEntity, TRequest> : IRequestHandler<TRequest, CommandResult>
        where TId : IComparable, IComparable<TId>, IEquatable<TId>, IConvertible
        where TEntity : class, IEntity<TId>
        where TRequest : class, IPutCommand<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PutCommandHandler{TId, TEntity, TRequest}"/> class
        /// </summary>
        /// <param name="repository">Entity repository</param>
        protected PutCommandHandler(IEntityRepository<TEntity, TId> repository)
        {
            this.Repository = repository;
        }

        /// <summary>
        /// Gets the entity repository
        /// </summary>
        protected IEntityRepository<TEntity, TId> Repository { get; }

        /// <summary>
        /// Handles the put request
        /// </summary>
        /// <param name="request">Put request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>An <see cref="CommandResult"/> that reports success and any validation errors it was a bad request</returns>
        public async Task<CommandResult> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var domainEntity = await this.Repository.RetrieveById(request.Id, cancellationToken);
            if (domainEntity == null) return CommandResult.NotFound();

            try
            {
                await this.BindToDomainEntityAndValidate(domainEntity, request, cancellationToken);

                await this.Repository.Update(domainEntity, cancellationToken);
            }
            catch (ValidationException ex)
            {
                return CommandResult.Fail(ex.Errors);
            }

            return CommandResult.Success();
        }

        /// <summary>
        /// Generate a domain entity from create entity request
        /// </summary>
        /// <param name="domainEntity">Domain entity read from the database to be updated</param>
        /// <param name="request">Create entity request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Asynchronous task</returns>
        /// <exception cref="ValidationException">Exception thrown when validation errors occur</exception>
        protected abstract Task BindToDomainEntityAndValidate(
            TEntity domainEntity,
            TRequest request,
            CancellationToken cancellationToken);
    }
}
