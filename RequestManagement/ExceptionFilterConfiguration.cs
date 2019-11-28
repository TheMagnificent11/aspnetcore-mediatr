using System;
using Microsoft.AspNetCore.Mvc;

namespace RequestManagement
{
    /// <summary>
    /// Exception Filter Configuration
    /// </summary>
    public static class ExceptionFilterConfiguration
    {
        /// <summary>
        /// Adds <see cref="ExceptionFilter"/> to MVC
        /// </summary>
        /// <param name="options">MVC options</param>
        public static void AddExceptionFilter(this MvcOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            options.Filters.Add<ExceptionFilter>();
        }
    }
}
