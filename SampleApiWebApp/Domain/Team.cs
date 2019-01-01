using System;
using System.Collections.Generic;
using System.Linq;
using EntityManagement.Abstractions;
using RequestManagement;
using SampleApiWebApp.Domain.Validators;

namespace SampleApiWebApp.Domain
{
    public class Team : ITeam, IEntity<long>
    {
        public long Id { get; protected set; }

        public string Name { get; protected set; }

        public IList<Player> Players { get; protected set; }

        public static Team CreateTeam(string teamName)
        {
            var team = new Team { Name = teamName };

            var errors = ValidateTeam(team);
            if (errors.Any()) throw new InvalidOperationException(errors.GetMultiLineErrorMessage());

            return team;
        }

        public static IDictionary<string, IEnumerable<string>> ValidateTeam(Team team)
        {
            var validator = new TeamValidator();

            return validator.ValidateEntity(team);
        }

        public void ChangeName(Team team, string newName)
        {
            if (team == null) throw new ArgumentNullException(nameof(team));

            team.Name = newName;

            var errors = ValidateTeam(team);
            if (errors.Any()) throw new InvalidOperationException(errors.GetMultiLineErrorMessage());
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
