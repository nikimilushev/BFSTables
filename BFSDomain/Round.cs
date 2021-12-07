using System;
using System.Collections.Generic;
using System.Linq;

namespace BFSDomain
{
    public class Round
    {
        private readonly List<Match> _matches;

        public Round()
        {
            _matches = new List<Match>();
        }

        public void AddMatch(Match match)
        {
            _matches.Add(match);
        }

        public List<TeamScore> GetTeamScores()
        {
            return _matches
                        .SelectMany(x => x.GetTeamsScore())
                        .OrderByDescending(x => x.P)
                        .ThenByDescending(x => x.GD)
                        .ThenBy(x => x.Team.ToString()).ToList();
        }

        public override string ToString()
        {
            return
                string.Join("\n", GetTeamScores());
        }
    }
}