using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RequestManagement
{
    /// <summary>
    /// Exception Filter
    /// </summary>
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        /// <summary>
        /// Handles exceptions thrown in API
        /// </summary>
        /// <param name="context">Exception contect</param>
        /// <returns>Asynchronous task</returns>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context?.Exception == null) return Task.CompletedTask;

            switch (context.Exception)
            {
                case ValidationException validationException:
                    context.Result = HandleValidationException(validationException);
                    break;

                case InvalidOperationException invalidOperationException:
                    context.Result = new BadRequestObjectResult(invalidOperationException.Message);
                    break;
            }

            return Task.CompletedTask;
        }

        private static IActionResult HandleValidationException(ValidationException exception)
        {
            var errors = exception.Errors
                .ToList()
                .GetErrors();

            var operationResult = OperationResult.Fail(errors);

            return new BadRequestObjectResult(operationResult.ToProblemDetails());
        }
    }
}
