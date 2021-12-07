using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFSDomain
{
    public class Match
    {
        private readonly Team _home;
        private readonly Team _away;
        private readonly int _homeGoals;
        private readonly int _awayGoals;

        public Match(Team home, Team away, int homeGoals, int awayGoals)
        {
            _home = home;
            _away = away;
            _homeGoals = homeGoals;
            _awayGoals = awayGoals;
        }

        public List<TeamScore> GetTeamsScore()
        {
            return new List<TeamScore>
            {
                new TeamScore(_home, _homeGoals, _awayGoals),
                new TeamScore(_away, _awayGoals, _homeGoals)
            };
        }
    }
}
