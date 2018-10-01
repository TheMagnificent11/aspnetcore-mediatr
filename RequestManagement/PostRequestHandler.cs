using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EntityManagement;
using MediatR;

namespace RequestManagement
{
    /// <summary>
    /// Post Request Handler
    /// </summary>
    /// <typeparam name="TId">Database entity ID type</typeparam>
    /// <typeparam name="TEntity">Database entity type</typeparam>
    /// <typeparam name="TRequest">Post request type</typeparam>
    public class PostRequestHandler<TId, TEntity, TRequest> :
        IRequestHandler<TRequest, OperationResult<TId>>
        where TId : IComparable, IComparable<TId>, IEquatable<TId>, IConvertible
        where TEntity : class, IEntity<TId>
        where TRequest : class, IPostRequest<TId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostRequestHandler{TId, TEntity, TRequest}"/> class
        /// </summary>
        /// <param name="repository">Entity repository</param>
        /// <param name="mappingProvider">Mapping provider</param>
        public PostRequestHandler(
            IEntityRepository<TEntity, TId> repository,
            IMapper mappingProvider)
        {
            Repository = repository;
            Mapper = mappingProvider;
        }

        /// <summary>
        /// Gets the entity repository
        /// </summary>
        protected IEntityRepository<TEntity, TId> Repository { get; }

        /// <summary>
        /// Gets the mapper
        /// </summary>
        protected IMapper Mapper { get; }

        /// <summary>
        /// Handles post requests
        /// </summary>
        /// <param name="request">Post request</param>
        /// <param name="cancellationToken">Canellation token</param>
        /// <returns>
        /// Operation result containing the ID of the created entity if successful,
        /// otherwise bad request operation result containing errors collection
        /// </returns>
        public async Task<OperationResult<TId>> Handle(
            TRequest request,
            CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var entity = Mapper.Map<TEntity>(request);

            await Repository.Create(entity);

            return OperationResult.Success(entity.Id);
        }
    }
}
