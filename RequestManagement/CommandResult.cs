using System;
using System.Collections.Generic;
using System.Net;
using FluentValidation.Extensions;
using FluentValidation.Results;

namespace RequestManagement
{
    /// <summary>
    /// Command Result
    /// </summary>
    public class CommandResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful
        /// </summary>
        public bool IsSuccess { get; protected set; }

        /// <summary>
        /// Gets or sets the HTTP status
        /// </summary>
        public HttpStatusCode Status { get; protected set; }

        /// <summary>
        /// Gets or sets a the operation errors
        /// </summary>
        public IDictionary<string, IEnumerable<string>> Errors { get; protected set; }

        /// <summary>
        /// Creates a <see cref="HttpStatusCode.OK"/> <see cref="CommandResult"/>
        /// </summary>
        /// <returns>A <see cref="HttpStatusCode.OK"/> <see cref="CommandResult"/></returns>
        public static CommandResult Success()
        {
            return new CommandResult()
            {
                IsSuccess = true,
                Status = HttpStatusCode.OK
            };
        }

        /// <summary>
        /// Creates a <see cref="HttpStatusCode.OK"/> <see cref="CommandResult{T}"/> with data
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="data">Data</param>
        /// <returns>A <see cref="HttpStatusCode.OK"/> <see cref="CommandResult{T}"/> with data</returns>
        public static CommandResult<T> Success<T>(T data)
        {
            return new CommandResult<T>(data)
            {
                IsSuccess = true,
                Status = HttpStatusCode.OK
            };
        }

        /// <summary>
        /// Creates a <see cref="HttpStatusCode.BadRequest"/> <see cref="CommandResult"/> with a set of errors
        /// </summary>
        /// <param name="errors">Errors</param>
        /// <returns>A <see cref="HttpStatusCode.BadRequest"/> <see cref="CommandResult"/></returns>
        public static CommandResult Fail(IDictionary<string, IEnumerable<string>> errors)
        {
            if (errors == null) throw new ArgumentNullException(nameof(errors));

            return new CommandResult()
            {
                IsSuccess = false,
                Status = HttpStatusCode.BadRequest,
                Errors = errors
            };
        }

        /// <summary>
        /// Creates a <see cref="HttpStatusCode.BadRequest"/> <see cref="CommandResult{T}"/> with a set of errors
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="errors">Errors</param>
        /// <returns>A <see cref="HttpStatusCode.BadRequest"/> <see cref="CommandResult{T}"/> containing no data</returns>
        public static CommandResult<T> Fail<T>(IDictionary<string, IEnumerable<string>> errors)
        {
            if (errors == null) throw new ArgumentNullException(nameof(errors));

            return new CommandResult<T>(default(T))
            {
                IsSuccess = false,
                Status = HttpStatusCode.BadRequest,
                Errors = errors
            };
        }

        /// <summary>
        /// Creates a <see cref="HttpStatusCode.BadRequest"/> <see cref="CommandResult"/> with a set of errors
        /// </summary>
        /// <param name="validationErrors">Validation errors</param>
        /// <returns>A <see cref="HttpStatusCode.BadRequest"/> <see cref="CommandResult"/></returns>
        public static CommandResult Fail(IEnumerable<ValidationFailure> validationErrors)
        {
            if (validationErrors == null) throw new ArgumentNullException(nameof(validationErrors));

            return Fail(validationErrors.GetErrors());
        }

        /// <summary>
        /// Creates a <see cref="HttpStatusCode.BadRequest"/> <see cref="CommandResult{T}"/> with a set of errors
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="validationErrors">Validation errors</param>
        /// <returns>A <see cref="HttpStatusCode.BadRequest"/> <see cref="CommandResult{T}"/> containing no data</returns>
        public static CommandResult<T> Fail<T>(IEnumerable<ValidationFailure> validationErrors)
        {
            if (validationErrors == null) throw new ArgumentNullException(nameof(validationErrors));

            return Fail<T>(validationErrors.GetErrors());
        }

        /// <summary>
        /// Creates a <see cref="HttpStatusCode.BadRequest"/> <see cref="CommandResult"/> with a single non-field-specific error
        /// </summary>
        /// <param name="errorMessage">Error message</param>
        /// <returns>A <see cref="HttpStatusCode.BadRequest"/> <see cref="CommandResult"/></returns>
        public static CommandResult Fail(string errorMessage)
        {
            if (errorMessage == null) throw new ArgumentNullException(nameof(errorMessage));

            var errors = new Dictionary<string, IEnumerable<string>>()
            {
                { string.Empty, new string[] { errorMessage } }
            };

            return Fail(errors);
        }

        /// <summary>
        /// Creates a <see cref="HttpStatusCode.BadRequest"/> <see cref="CommandResult{T}"/> with a single non-field-specific error
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <param name="errorMessage">Error message</param>
        /// <returns>A <see cref="HttpStatusCode.BadRequest"/> <see cref="CommandResult{T}"/> containing no data</returns>
        public static CommandResult<T> Fail<T>(string errorMessage)
        {
            if (errorMessage == null) throw new ArgumentNullException(nameof(errorMessage));

            var errors = new Dictionary<string, IEnumerable<string>>()
            {
                { string.Empty, new string[] { errorMessage } }
            };

            return Fail<T>(errors);
        }

        /// <summary>
        /// Creates a <see cref="HttpStatusCode.NotFound"/> failure <see cref="CommandResult"/>
        /// </summary>
        /// <returns>A <see cref="HttpStatusCode.NotFound"/> <see cref="CommandResult"/></returns>
        public static CommandResult NotFound()
        {
            return new CommandResult
            {
                IsSuccess = false,
                Status = HttpStatusCode.NotFound
            };
        }

        /// <summary>
        /// Creates a <see cref="HttpStatusCode.NotFound"/> failure <see cref="CommandResult{T}"/>
        /// </summary>
        /// <typeparam name="T">Data type</typeparam>
        /// <returns>A <see cref="HttpStatusCode.NotFound"/> <see cref="CommandResult{T}"/></returns>
        public static CommandResult<T> NotFound<T>()
        {
            return new CommandResult<T>(default(T))
            {
                IsSuccess = false,
                Status = HttpStatusCode.NotFound
            };
        }
    }

#pragma warning disable SA1402 // File may only contain a single class
    /// <summary>
    /// Operation Result
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    public class CommandResult<T> : CommandResult
#pragma warning restore SA1402 // File may only contain a single class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResult{T}"/> class
        /// </summary>
        /// <param name="data">Data</param>
        internal CommandResult(T data)
        {
            this.Data = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResult{T}"/> class
        /// </summary>
        /// <param name="errors">Errors</param>
        internal CommandResult(IDictionary<string, IEnumerable<string>> errors)
        {
            this.Errors = errors;
            this.IsSuccess = false;
            this.Status = HttpStatusCode.BadRequest;
        }

        /// <summary>
        /// Gets or sets the operation data
        /// </summary>
        public T Data { get; protected set; }
    }
}
