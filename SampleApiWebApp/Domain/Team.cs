using System.Collections.Generic;
using EntityManagement;

namespace SampleApiWebApp.Domain
{
    public class Team : IEntity<long>
    {
        public const int NameMaxLength = 50;

        public long Id { get; set; }

        public string Name { get; set; }

        public IList<Player> Players { get; set; }
    }
}
