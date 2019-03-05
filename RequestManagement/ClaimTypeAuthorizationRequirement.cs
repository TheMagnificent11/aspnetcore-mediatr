namespace RequestManagement
{
    /// <summary>
    /// Claim Type Authorization Requirement
    /// </summary>
    public class ClaimTypeAuthorizationRequirement : IClaimTypeAuthorizationRequirement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimTypeAuthorizationRequirement"/> class
        /// </summary>
        /// <param name="claimType">Claim type</param>
        public ClaimTypeAuthorizationRequirement(string claimType)
        {
            ClaimType = claimType;
        }

        /// <summary>
        /// Gets the claim type
        /// </summary>
        public string ClaimType { get; }
    }
}
