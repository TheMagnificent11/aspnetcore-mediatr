using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace RequestManagement
{
    /// <summary>
    /// Operational Result Extension Methods
    /// </summary>
    public static class OperationResultExtensions
    {
        /// <summary>
        /// Converts to a <see cref="ValidationProblemDetails"/> object
        /// </summary>
        /// <param name="result">Operation result to convert</param>
        /// <returns>Validation problem details</returns>
        public static ValidationProblemDetails ToProblemDetails(this OperationResult result)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));

            var problemDetails = new ValidationProblemDetails()
            {
                Status = (int)HttpStatusCode.BadRequest
            };

            if (problemDetails.Errors != null)
            {
                result.Errors
                   .ToList()
                   .ForEach(i => problemDetails.Errors.Add(i.Key, i.Value.ToArray()));
            }

            return problemDetails;
        }
    }
}
