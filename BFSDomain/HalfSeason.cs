using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFSDomain
{
    public class HalfSeason
    {
        private readonly List<Round> _rounds;

        public HalfSeason()
        {
            _rounds = new List<Round>();
        }

        public void AddRound(Round round)
        {
            _rounds.Add(round);
        }

        public Table GetTable()
        {
            return new Table(_rounds);
        }
    }
}
