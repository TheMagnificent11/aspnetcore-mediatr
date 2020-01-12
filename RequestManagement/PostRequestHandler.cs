using System;
using System.Threading;
using System.Threading.Tasks;
using EntityManagement.Core;
using FluentValidation;
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
            this.Repository = repository;
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

            try
            {
                var entity = await this.GenerateAndValidateDomainEntity(request, cancellationToken);

                await this.Repository.Create(entity, cancellationToken);

                return OperationResult.Success(entity.Id);
            }
            catch (ValidationException ex)
            {
                return OperationResult.Fail<TId>(ex.Errors);
            }
        }

        /// <summary>
        /// Generate a domain entity from create entity request
        /// </summary>
        /// <param name="request">Create entity request</param>
        /// <param name="cancellationToken">Canellation token</param>
        /// <returns>Entity to be created</returns>
        /// <exception cref="ValidationException">Exception thrown when validation errors occur</exception>
        protected abstract Task<TEntity> GenerateAndValidateDomainEntity(TRequest request, CancellationToken cancellationToken);
    }
}
