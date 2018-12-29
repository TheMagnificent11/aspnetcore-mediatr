using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EntityManagement;
using MediatR;

namespace RequestManagement
{
    /// <summary>
    /// Delete Request Handler
    /// </summary>
    /// <typeparam name="TId">Database entity ID type</typeparam>
    /// <typeparam name="TEntity">Database entity type</typeparam>
    /// <typeparam name="TRequest">Put request type</typeparam>
    public abstract class DeleteRequestHandler<TId, TEntity, TRequest> : IRequestHandler<TRequest, OperationResult>
        where TId : IComparable, IComparable<TId>, IEquatable<TId>, IConvertible
        where TEntity : class, IEntity<TId>
        where TRequest : class, IPutRequest<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRequestHandler{TId, TEntity, TRequest}"/> class
        /// </summary>
        /// <param name="repository">Entity repository</param>
        protected DeleteRequestHandler(IEntityRepository<TEntity, TId> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Gets the entity repository
        /// </summary>
        protected IEntityRepository<TEntity, TId> Repository { get; }

        /// <summary>
        /// Handles the delete request
        /// </summary>
        /// <param name="request">Delete request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>An <see cref="OperationResult"/> that reports success and any validation errors it was a bad request</returns>
        public async Task<OperationResult> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var domainEntity = await Repository.RetrieveById(request.Id);
            if (domainEntity == null) return OperationResult.NotFound();

            var validationErrors = await ValidateDeletion(domainEntity, request, cancellationToken);
            if (validationErrors != null && validationErrors.Any()) return OperationResult.Fail(validationErrors);

            await Repository.Delete(domainEntity.Id);

            return OperationResult.Success();
        }

        /// <summary>
        /// Validates whether deletion is allowed
        /// </summary>
        /// <param name="domainEntity">Entity to delete</param>
        /// <param name="request">Delete request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Dictionary of validation errors keyed by request field name</returns>
        protected virtual Task<IDictionary<string, IEnumerable<string>>> ValidateDeletion(
            TEntity domainEntity,
            TRequest request,
            CancellationToken cancellationToken)
        {
            IDictionary<string, IEnumerable<string>> validationErrors = new Dictionary<string, IEnumerable<string>>();
            return Task.FromResult(validationErrors);
        }
    }
}
