using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace RequestManagement
{
    /// <summary>
    /// Validation Behaviour
    /// </summary>
    /// <typeparam name="TRequest">Request type</typeparam>
    public class ValidationBehavior<TRequest> : IPipelineBehavior<TRequest, OperationResult>
        where TRequest : IRequest<OperationResult>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest}"/> class
        /// </summary>
        /// <param name="validators">Validators</param>
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            Validators = validators;
        }

        private IEnumerable<IValidator<TRequest>> Validators { get; }

        /// <summary>
        /// Handles request
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="next">Next behaviour</param>
        /// <returns>Response</returns>
        public Task<OperationResult> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<OperationResult> next)
        {
            var context = new ValidationContext(request);

            var failures = Validators
                .Select(i => i.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(i => i != null)
                .ToList();

            if (failures.Any())
            {
                var errors = failures.GetErrors();
                return Task.FromResult(OperationResult.Fail(errors));
            }

            return next();
        }
    }

#pragma warning disable SA1402
    /// File may only contain a single class
    /// <summary>
    /// Validation Behaviour
    /// </summary>
    /// <typeparam name="TRequest">Request type</typeparam>
    /// <typeparam name="TResponseData">Response data type</typeparam>
    public class ValidationBehavior<TRequest, TResponseData> :
#pragma warning restore SA1402 // File may only contain a single class
        IPipelineBehavior<TRequest, OperationResult<TResponseData>>
        where TRequest : IRequest<OperationResult<TResponseData>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest, TResponseData}"/> class
        /// </summary>
        /// <param name="validators">Validators</param>
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            Validators = validators;
        }

        private IEnumerable<IValidator<TRequest>> Validators { get; }

        /// <summary>
        /// Handles request
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="next">Next behaviour</param>
        /// <returns>Response</returns>
        public Task<OperationResult<TResponseData>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<OperationResult<TResponseData>> next)
        {
            var context = new ValidationContext(request);

            var failures = Validators
                .Select(i => i.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(i => i != null)
                .ToList();

            if (failures.Any())
            {
                var errors = failures.GetErrors();
                return Task.FromResult(new OperationResult<TResponseData>(errors));
            }

            return next();
        }
    }

#pragma warning disable SA1402 // File may only contain a single class
    /// <summary>
    /// Validation Extensions
    /// </summary>
    internal static class ValidationExtensions
#pragma warning restore SA1402 // File may only contain a single class
    {
        /// <summary>
        /// Get errors from validation failures
        /// </summary>
        /// <param name="failures">Validation failures</param>
        /// <returns>Dictionary of errors</returns>
        public static IDictionary<string, IEnumerable<string>> GetErrors(this IList<ValidationFailure> failures)
        {
            var errors = new Dictionary<string, IEnumerable<string>>();

            foreach (var item in failures)
            {
                var propertyErrors = default(List<string>);

                if (errors.ContainsKey(item.PropertyName))
                {
                    propertyErrors = errors[item.PropertyName].ToList();
                }
                else
                {
                    propertyErrors = new List<string>();
                }

                propertyErrors.Add(item.ErrorMessage);

                if (errors.ContainsKey(item.PropertyName))
                {
                    errors[item.PropertyName] = propertyErrors;
                }
                else
                {
                    errors.Add(item.PropertyName, propertyErrors);
                }
            }

            return errors;
        }
    }
}
