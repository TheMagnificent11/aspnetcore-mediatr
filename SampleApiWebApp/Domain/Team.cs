using System;
using System.Collections.Generic;
using EntityManagement.Abstractions;

namespace SampleApiWebApp.Domain
{
    public class Team : IEntity<long>
    {
        public long Id { get; protected set; }

        public string Name { get; protected set; }

        public IList<Player> Players { get; protected set; }

        public static Team CreateTeam(string teamName)
        {
            ValidateTeamName(teamName);

            return new Team { Name = teamName };
        }

        public void ChangeName(Team team, string newName)
        {
            if (team == null) throw new ArgumentNullException(nameof(team));

            ValidateTeamName(newName);

            team.Name = newName;
        }

        private static void ValidateTeamName(string teamName)
        {
            if (teamName == null) throw new ArgumentNullException(nameof(teamName));

            if (teamName.Length > FieldMaxLenghts.Name)
            {
                throw new ArgumentException($"Team names cannot be longer than {FieldMaxLenghts.Name} characters");
            }
        }

        public static class FieldMaxLenghts
        {
            public const int Name = 50;
        }

        public static class ErrorMessages
        {
            public const string NameNotUniqueFormat = "Team Name '{0}' is not unqiue";
        }
    }
}
