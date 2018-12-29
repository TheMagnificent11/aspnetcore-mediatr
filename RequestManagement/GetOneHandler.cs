using System;
using System.Threading;
using System.Threading.Tasks;
using EntityManagement;
using MediatR;

namespace RequestManagement
{
    /// <summary>
    /// Get One Handler
    /// </summary>
    /// <typeparam name="TId">Database entity ID type</typeparam>
    /// <typeparam name="TEntity">Database entity type</typeparam>
    /// <typeparam name="TResponseEntity">Entity response type</typeparam>
    /// <typeparam name="TRequest">Request type</typeparam>
    public abstract class GetOneHandler<TId, TEntity, TResponseEntity, TRequest> :
        IRequestHandler<TRequest, OperationResult<TResponseEntity>>
        where TId : IComparable, IComparable<TId>, IEquatable<TId>, IConvertible
        where TEntity : class, IEntity<TId>
        where TResponseEntity : class
        where TRequest : class, IGetOneRequest<TId, TResponseEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetOneHandler{TId, TEntity, TResponseEntity, TRequest}"/> class
        /// </summary>
        /// <param name="repository">Entity repository</param>
        protected GetOneHandler(IEntityRepository<TEntity, TId> repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Gets the entity repository
        /// </summary>
        protected IEntityRepository<TEntity, TId> Repository { get; }

        /// <summary>
        /// Handles get one entity requests
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Operation result containing the response entity as the data if successful,
        /// otherwise not found operation result
        /// </returns>
        public async Task<OperationResult<TResponseEntity>> Handle(
            TRequest request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var entity = await Repository.RetrieveById(request.Id);
            if (entity == null) return OperationResult.NotFound<TResponseEntity>();

            var result = GenerateResponseEntity(entity);

            return OperationResult.Success(result);
        }

        /// <summary>
        /// Generates a response entity from the domain entity
        /// </summary>
        /// <param name="entity">Domain entity</param>
        /// <returns>Response entity</returns>
        protected abstract TResponseEntity GenerateResponseEntity(TEntity entity);
    }
}
