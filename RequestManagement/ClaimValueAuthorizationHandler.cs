using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;

namespace RequestManagement
{
    /// <summary>
    /// Claim Value Authorization Handler
    /// </summary>
    /// <typeparam name="T"><see cref="IAuthorizationRequirement"/> type</typeparam>
    public class ClaimValueAuthorizationHandler<T> : BaseClaimTypeAuthorizationHandler<T>
        where T : IClaimValueAuthorizationRequirement
    {
        /// <summary>
        /// Determines whether the authenticated user/client has the appropriate claim to meet the <paramref name="requirement"/>
        /// </summary>
        /// <param name="requirement">Authorization requirement</param>
        /// <param name="claims">User/client claims</param>
        /// <param name="routeValues">Route values (can be null)</param>
        /// <returns>Whether the user/client meets the authorization requirement</returns>
        protected override Task<bool> HasClaimType(
            T requirement,
            IEnumerable<Claim> claims,
            RouteValueDictionary routeValues)
        {
            if (requirement == null) throw new ArgumentNullException(nameof(requirement));

            if (claims == null) return Task.FromResult(false);

            var matchingClaim = claims.FirstOrDefault(i =>
                i.Type == requirement.ClaimType &&
                i.Value == requirement.ClaimValue);

            return Task.FromResult(matchingClaim != null);
        }
    }
}
