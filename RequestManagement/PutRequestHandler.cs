using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntityManagement.Abstractions;
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

            var validationErrors = await ValidateRequest(domainEntity, request, cancellationToken);
            if (validationErrors != null && validationErrors.Any()) return OperationResult.Fail(validationErrors);

            BindToDomainEntity(domainEntity, request);

            await Repository.Update(domainEntity, cancellationToken);

            return OperationResult.Success();
        }

        /// <summary>
        /// Generate a domain entity from create entity request
        /// </summary>
        /// <param name="domainEntity">Domain entity read from the database to be updated</param>
        /// <param name="request">Create entity request</param>
        protected abstract void BindToDomainEntity(TEntity domainEntity, TRequest request);

        /// <summary>
        /// Validate the request
        /// </summary>
        /// <param name="domainEntity">Domain entity read from the database</param>
        /// <param name="request">Reqest to validate</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Dictionary of validation errors keyed by request field name</returns>
        protected virtual Task<IDictionary<string, IEnumerable<string>>> ValidateRequest(
            TEntity domainEntity,
            TRequest request,
            CancellationToken cancellationToken)
        {
            IDictionary<string, IEnumerable<string>> validationErrors = new Dictionary<string, IEnumerable<string>>();
            return Task.FromResult(validationErrors);
        }
    }
}
