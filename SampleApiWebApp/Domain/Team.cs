using System.Collections.Generic;
using EntityManagement.Abstractions;

namespace SampleApiWebApp.Domain
{
    public class Team : IEntity<long>
    {
        public const int NameMaxLength = 50;

        public long Id { get; protected set; }

        public string Name { get; protected set; }

        public IList<Player> Players { get; protected set; }
    }
}
