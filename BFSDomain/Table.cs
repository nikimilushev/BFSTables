using System.Collections.Generic;
using System.Linq;

namespace BFSDomain
{
    public class Table
    {
        private readonly List<Row> _rows;
        //private readonly Row _headerRow;

        public Table(List<Round> rounds)
        {
            //_headerRow = new Row();
            //_headerRow.AddColumn("Team");
            //_headerRow.AddColumn("Played");
            //_headerRow.AddColumn("Won");
            //_headerRow.AddColumn("Drawn");
            //_headerRow.AddColumn("Lost");
            //_headerRow.AddColumn("GF");
            //_headerRow.AddColumn("GA");
            //_headerRow.AddColumn("GD");
            //_headerRow.AddColumn("Points");

            var rows = rounds.SelectMany(x => x.GetTeamScores())
                .GroupBy(x => x.Team.ToString())
                .Select(x => new
                {
                    Team = x.Key,
                    C = x.Count(),
                    W = x.Sum(y => y.W),
                    D = x.Sum(y => y.D),
                    L = x.Sum(y => y.L),
                    GF = x.Sum(y => y.GF),
                    GA = x.Sum(y => y.GA),
                    GD = x.Sum(y => y.GD),
                    P = x.Sum(y => y.P)
                })
                .OrderByDescending(x => x.P)
                .ThenByDescending(x => x.GD)
                .ThenBy(x => x.Team);

            _rows = rows.Select(x =>
                {
                    var row = new Row();
                    row.AddColumn(x.Team);
                    row.AddColumn(x.C);
                    row.AddColumn(x.W);
                    row.AddColumn(x.D);
                    row.AddColumn(x.L);
                    row.AddColumn(x.GF);
                    row.AddColumn(x.GA);
                    row.AddColumn(x.GD);
                    row.AddColumn(x.P);
                    return row;
                })
                .ToList();
        }

        public IEnumerable<Row> Rows => _rows;

        public override string ToString()
        {
            return 
                string.Join("\n", _rows.Select(x => "| " + string.Join(" | ",x.Columns) + " |"));
        }
    }
}