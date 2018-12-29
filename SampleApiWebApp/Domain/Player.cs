using EntityManagement;

namespace SampleApiWebApp.Domain
{
    public class Player : IEntity<long>
    {
        public const int NameMaxLength = 50;

        public long Id { get; set; }

        public string GivenName { get; set; }

        public string Surname { get; set; }

        public long TeamId { get; set; }

        public Team Team { get; set; }

        public int SquadNumber { get; set; }
    }
}
