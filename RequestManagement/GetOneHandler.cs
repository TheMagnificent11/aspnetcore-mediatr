using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EntityManagement;
using MediatR;

namespace RequestManagement
{
    /// <summary>
    /// Get One Handler
    /// </summary>
    /// <typeparam name="TId">Entity ID type</typeparam>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TRequest">Request type</typeparam>
    /// <typeparam name="TResponseEntity">Entity response type</typeparam>
    public class GetOneHandler<TId, TEntity, TRequest, TResponseEntity> : IRequestHandler<TRequest, OperationResult<TResponseEntity>>
        where TId : IComparable, IComparable<TId>, IEquatable<TId>, IConvertible
        where TEntity : class, IEntity<TId>
        where TRequest : class, IGetOneRequest<TId, TResponseEntity>
        where TResponseEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetOneHandler{TId, TEntity, TRequest, TResponseEntity}"/> class
        /// </summary>
        /// <param name="repository">Entity repository</param>
        /// <param name="mappingProvider">Mapping provider</param>
        public GetOneHandler(
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
        /// Handles get one entity requests
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Operation result containing the response entity as the data if successful, otherwise not found operation result</returns>
        public async Task<OperationResult<TResponseEntity>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var entity = await Repository.RetrieveById(request.Id);
            if (entity == null) return OperationResult.NotFound<TResponseEntity>();

            var result = Mapper.Map<TResponseEntity>(entity);

            return OperationResult.Success(result);
        }
    }
}
