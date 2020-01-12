using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EntityManagement.Core;
using MediatR;

namespace RequestManagement
{
    /// <summary>
    /// Get All Query Handler
    /// </summary>
    /// <typeparam name="TId">Database entity ID type</typeparam>
    /// <typeparam name="TEntity">Database entity type</typeparam>
    /// <typeparam name="TResponseEntity">Entity response type</typeparam>
    /// <typeparam name="TRequest">Request type</typeparam>
    public abstract class GetAllQueryHandler<TId, TEntity, TResponseEntity, TRequest> :
        IRequestHandler<TRequest, CommandResult<IEnumerable<TResponseEntity>>>
        where TId : IComparable, IComparable<TId>, IEquatable<TId>, IConvertible
        where TEntity : class, IEntity<TId>
        where TResponseEntity : class
        where TRequest : class, IGetAllQuery<TResponseEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllQueryHandler{TId, TEntity, TResponseEntity, TRequest}"/> class
        /// </summary>
        /// <param name="repository">Entity repository</param>
        protected GetAllQueryHandler(IEntityRepository<TEntity, TId> repository)
        {
            this.Repository = repository;
        }

        /// <summary>
        /// Gets entity repository
        /// </summary>
        protected IEntityRepository<TEntity, TId> Repository { get; }

        /// <summary>
        /// Handlers get all request
        /// </summary>
        /// <param name="request">Get all request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>An enumerable list of entities</returns>
        public async Task<CommandResult<IEnumerable<TResponseEntity>>> Handle(
            TRequest request,
            CancellationToken cancellationToken)
        {
            var domainEntities = await this.Repository.RetrieveAll(cancellationToken);
            var responseEntities = this.MapEntities(domainEntities);

            return CommandResult.Success<IEnumerable<TResponseEntity>>(responseEntities);
        }

        /// <summary>
        /// Maps domain entities to response entities
        /// </summary>
        /// <param name="domainEntities">Domain entities</param>
        /// <returns>Response entities</returns>
        protected abstract IEnumerable<TResponseEntity> MapEntities(IEnumerable<TEntity> domainEntities);
    }
}
