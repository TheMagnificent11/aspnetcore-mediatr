using System;
using System.Threading;
using System.Threading.Tasks;
using EntityManagement.Core;
using FluentValidation;
using MediatR;

namespace RequestManagement
{
    /// <summary>
    /// Put Request Handler
    /// </summary>
    /// <typeparam name="TId">Database entity ID type</typeparam>
    /// <typeparam name="TEntity">Database entity type</typeparam>
    /// <typeparam name="TRequest">Put request type</typeparam>
    public abstract class PutRequestHandler<TId, TEntity, TRequest> : IRequestHandler<TRequest, OperationResult>
        where TId : IComparable, IComparable<TId>, IEquatable<TId>, IConvertible
        where TEntity : class, IEntity<TId>
        where TRequest : class, IPutRequest<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PutRequestHandler{TId, TEntity, TRequest}"/> class
        /// </summary>
        /// <param name="repository">Entity repository</param>
        protected PutRequestHandler(IEntityRepository<TEntity, TId> repository)
        {
            Repository = repository;
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
        /// <returns>An <see cref="OperationResult"/> that reports success and any validation errors it was a bad request</returns>
        public async Task<OperationResult> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var domainEntity = await Repository.RetrieveById(request.Id, cancellationToken);
            if (domainEntity == null) return OperationResult.NotFound();

            try
            {
                await BindToDomainEntityAndValidate(domainEntity, request, cancellationToken);

                await Repository.Update(domainEntity, cancellationToken);
            }
            catch (ValidationException ex)
            {
                return OperationResult.Fail(ex.Errors);
            }

            return OperationResult.Success();
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
