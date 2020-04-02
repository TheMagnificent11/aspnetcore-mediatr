using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace RequestManagement
{
    /// <summary>
    /// Validation Behaviour
    /// </summary>
    /// <typeparam name="TRequest">Request type</typeparam>
    /// <typeparam name="TResponse">Response type</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest, TResponse}"/> class
        /// </summary>
        /// <param name="validators">Validators</param>
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.Validators = validators;
        }

        private IEnumerable<IValidator<TRequest>> Validators { get; }

        /// <summary>
        /// Handles request
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="next">Next behaviour</param>
        /// <returns>Response</returns>
        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            var tasks = this.Validators.Select(v => v.ValidateAsync(request, cancellationToken));
            var results = await Task.WhenAll(tasks);
            var failures = results
                .SelectMany(result => result.Errors)
                .Where(i => i != null)
                .ToList();

            if (failures.Any()) throw new ValidationException(failures);

            return await next();
        }
    }
}
