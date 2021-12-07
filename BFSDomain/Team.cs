using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFSDomain
{
    public class Team
    {
        private readonly string _name;

        public Team(string name)
        {
            _name = name;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
