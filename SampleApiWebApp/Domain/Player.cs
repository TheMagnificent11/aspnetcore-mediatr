using EntityManagement.Abstractions;

namespace SampleApiWebApp.Domain
{
    public class Player : IEntity<long>
    {
        public long Id { get; protected set; }

        public string GivenName { get; protected set; }

        public string Surname { get; protected set; }

        public long TeamId { get; protected set; }

        public Team Team { get; protected set; }

        public int SquadNumber { get; protected set; }

        public static class FieldNameMaxLengths
        {
            public const int Name = 50;
        }
    }
}
