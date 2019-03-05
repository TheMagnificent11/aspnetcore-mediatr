namespace RequestManagement
{
    /// <summary>
    /// Claim Value Authorization Requirement
    /// </summary>
    public class ClaimValueAuthorizationRequirement : ClaimTypeAuthorizationRequirement, IClaimValueAuthorizationRequirement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimValueAuthorizationRequirement"/> class
        /// </summary>
        /// <param name="claimType">Claim type</param>
        /// <param name="claimValue">Claim value</param>
        public ClaimValueAuthorizationRequirement(string claimType, string claimValue)
            : base(claimType)
        {
            ClaimValue = claimValue;
        }

        /// <summary>
        /// Gets the claim value
        /// </summary>
        public string ClaimValue { get; }
    }
}
