namespace RequestManagement
{
    /// <summary>
    /// Claim Value Authorization Requirement Interface
    /// </summary>
    public interface IClaimValueAuthorizationRequirement : IClaimTypeAuthorizationRequirement
    {
        /// <summary>
        /// Gets the claim value
        /// </summary>
        string ClaimValue { get; }
    }
}
