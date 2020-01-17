﻿using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace RequestManagement
{
    /// <summary>
    /// Command Result Extension Methods
    /// </summary>
    public static class CommandResultExtensions
    {
        /// <summary>
        /// Converts to a <see cref="ValidationProblemDetails"/> object
        /// </summary>
        /// <param name="result">Command result to convert</param>
        /// <returns>Validation problem details</returns>
        public static ValidationProblemDetails ToProblemDetails(this CommandResult result)
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

        /// <summary>
        /// Converts to an <see cref="IActionResult"/> object
        /// </summary>
        /// <param name="result">Operation result to convert</param>
        /// <returns>Action result</returns>
        public static IActionResult ToActionResult(this CommandResult result)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));

            switch (result.Status)
            {
                case HttpStatusCode.OK:
                    return new OkResult();

                case HttpStatusCode.NotFound:
                    return new NotFoundResult();

                case HttpStatusCode.BadRequest:
                    var problems = result.ToProblemDetails();
                    return new BadRequestObjectResult(problems);

                default:
                    return new StatusCodeResult((int)result.Status);
            }
        }

        /// <summary>
        /// Converts to an <see cref="IActionResult"/> object
        /// </summary>
        /// <typeparam name="T">Operation result data type</typeparam>
        /// <param name="result">Operation result to convert</param>
        /// <returns>Action result</returns>
        public static IActionResult ToActionResult<T>(this CommandResult<T> result)
        {
            if (result == null) throw new ArgumentNullException(nameof(result));

            switch (result.Status)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(result.Data);

                case HttpStatusCode.NotFound:
                    return new NotFoundResult();

                case HttpStatusCode.BadRequest:
                    var problems = result.ToProblemDetails();
                    return new BadRequestObjectResult(problems);

                default:
                    return new StatusCodeResult((int)result.Status);
            }
        }
    }
}