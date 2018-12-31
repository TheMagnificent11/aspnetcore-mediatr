namespace SampleApiWebApp.Controllers.Players
{
    public class Player
    {
        public long Id { get; set; }

        public string GivenName { get; set; }

        public string Surname { get; set; }

        public long TeamId { get; set; }

        public int SquadNumber { get; set; }
    }
}
