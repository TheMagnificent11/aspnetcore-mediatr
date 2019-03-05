using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace RequestManagement
{
    /// <summary>
    /// Base Claim Type Authorization Handler
    /// </summary>
    /// <typeparam name="T"><see cref="IAuthorizationRequirement"/> type</typeparam>
    public abstract class BaseClaimTypeAuthorizationHandler<T> : AuthorizationHandler<T>
       where T : IClaimTypeAuthorizationRequirement
    {
        /// <summary>
        /// Determines whether the user/client is authorized
        /// </summary>
        /// <param name="context">Authorization context</param>
        /// <param name="requirement">Authorization requirement</param>
        /// <returns>Asynchronous task</returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, T requirement)
        {
            if (context.User?.Claims == null)
            {
                context.Fail();
                return;
            }

            var routeValues = default(RouteValueDictionary);

            if (context.Resource is AuthorizationFilterContext)
            {
                routeValues = ((AuthorizationFilterContext)context.Resource).RouteData?.Values;
            }

            if (await this.HasClaimType(requirement, context.User.Claims, routeValues))
            {
                context.Succeed(requirement);
                return;
            }

            context.Fail();
        }

        /// <summary>
        /// Determines whether the authenticated user/client has the appropriate claim to meet the <paramref name="requirement"/>
        /// </summary>
        /// <param name="requirement">Authorization requirement</param>
        /// <param name="claims">User/client claims</param>
        /// <param name="routeValues">Route values (can be null)</param>
        /// <returns>Whether the user/client meets the authorization requirement</returns>
        protected abstract Task<bool> HasClaimType(T requirement, IEnumerable<Claim> claims, RouteValueDictionary routeValues);
    }
}
