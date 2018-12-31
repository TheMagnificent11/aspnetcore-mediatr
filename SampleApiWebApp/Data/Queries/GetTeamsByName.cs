using System;
using EntityManagement;
using SampleApiWebApp.Domain;

namespace SampleApiWebApp.Data.Queries
{
    public sealed class GetTeamsByName : BaseQuerySpecification<Team>
    {
        public GetTeamsByName(string teamName)
            : base(i => i.Name.Equals(teamName.Trim(), StringComparison.CurrentCultureIgnoreCase))
        {
        }
    }
}
