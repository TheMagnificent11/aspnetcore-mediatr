using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntityManagement.Core;
using MediatR;

namespace RequestManagement
{
    /// <summary>
    /// Post Request Handler
    /// </summary>
    /// <typeparam name="TId">Database entity ID type</typeparam>
    /// <typeparam name="TEntity">Database entity type</typeparam>
    /// <typeparam name="TRequestEntity">Request entity type</typeparam>
    /// <typeparam name="TRequest">Post request type</typeparam>
    public abstract class PostRequestHandler<TId, TEntity, TRequestEntity, TRequest> :
        IRequestHandler<TRequest, OperationResult<TId>>
        where TId : IComparable, IComparable<TId>, IEquatable<TId>, IConvertible
        where TEntity : class, IEntity<TId>
        where TRequestEntity : class
        where TRequest : class, IPostRequest<TId, TRequestEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostRequestHandler{TId, TEntity, TRequestEntity, TRequest}"/> class
        /// </summary>
        /// <param name="repository">Entity repository</param>
        protected PostRequestHandler(IEntityRepository<TEntity, TId> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Gets the entity repository
        /// </summary>
        protected IEntityRepository<TEntity, TId> Repository { get; }

        /// <summary>
        /// Handles post requests
        /// </summary>
        /// <param name="request">Post request</param>
        /// <param name="cancellationToken">Canellation token</param>
        /// <returns>
        /// An <see cref="OperationResult{T}"/> containing the ID of the created entity if successful,
        /// otherwise bad request operation result containing errors collection
        /// </returns>
        public async Task<OperationResult<TId>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var validationErrors = await ValidateRequest(request, cancellationToken);
            if (validationErrors != null && validationErrors.Any()) return OperationResult.Fail<TId>(validationErrors);

            var entity = GenerateDomainEntity(request);

            await Repository.Create(entity, cancellationToken);

            return OperationResult.Success(entity.Id);
        }

        /// <summary>
        /// Generate a domain entity from create entity request
        /// </summary>
        /// <param name="request">Create entity request</param>
        /// <returns>Entity to be created</returns>
        protected abstract TEntity GenerateDomainEntity(TRequest request);

        /// <summary>
        /// Validate the request
        /// </summary>
        /// <param name="request">Reqest to validate</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Dictionary of validation errors keyed by request field name</returns>
        protected virtual Task<IDictionary<string, IEnumerable<string>>> ValidateRequest(TRequest request, CancellationToken cancellationToken)
        {
            IDictionary<string, IEnumerable<string>> validationErrors = new Dictionary<string, IEnumerable<string>>();
            return Task.FromResult(validationErrors);
        }
    }
}
