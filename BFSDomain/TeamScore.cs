using System;

namespace BFSDomain
{
    public enum Outcome { Win, Draw, Loss };
    public class TeamScore
    {
        public TeamScore(Team team, int GF, int GA)
        {
            Team = team;
            this.GA = GA;
            this.GF = GF;
        }

        public Team Team { get; }

        public Outcome Outcome => GA == GF ? Outcome.Draw : GA > GF ? Outcome.Loss : Outcome.Win;
        public int GA { get; }
        public int GF { get; }
        public int GD => GF - GA;
        public int P => Outcome == Outcome.Win ? 3 : Outcome == Outcome.Draw ? 1 : 0;

        public int W => Outcome == Outcome.Win ? 1 : 0;
        public int L => Outcome == Outcome.Loss ? 1 : 0;
        public int D => Outcome == Outcome.Draw ? 1 : 0;

        public override string ToString()
        {
            return $"| {Team} | {W} | {D} | {L} | {GF} | {GA} | {GD} | {P} |";
        }
    }
}