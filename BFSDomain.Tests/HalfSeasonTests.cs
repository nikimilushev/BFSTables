using Xunit;

namespace BFSDomain.Tests
{
    public class HalfSeasonTests
    {
        [Fact]
        public void Empty_HalfSeason_Returns_NoData()
        {
            var sut = new HalfSeason();
            Assert.Equal("| Team | Played | Won | Drawn | Lost | GF | GA | GD | Points |", sut.GetTable().ToString());
        }
        
        [Fact]
        public void HalfSeason_WithOneMatch_Draw()
        {
            var expected = "| Team | Played | Won | Drawn | Lost | GF | GA | GD | Points |\n" +
                            "| Team 1 | 1 | 0 | 1 | 0 | 0 | 0 | 0 | 1 |\n" +
                            "| Team 2 | 1 | 0 | 1 | 0 | 0 | 0 | 0 | 1 |";
            var sut = new HalfSeason();
            var round = new Round();
            round.AddMatch(new Match(new Team("Team 1"), new Team("Team 2"), 0, 0));
            sut.AddRound(round);
            Assert.Equal(expected, sut.GetTable().ToString());
        }
        
        [Fact]
        public void HalfSeason_WithOneMatch_Win()
        {
            var expected = "| Team | Played | Won | Drawn | Lost | GF | GA | GD | Points |\n" +
                              "| Team 2 | 1 | 1 | 0 | 0 | 1 | 0 | 1 | 3 |\n" +
                              "| Team 1 | 1 | 0 | 0 | 1 | 0 | 1 | -1 | 0 |";
            var sut = new HalfSeason();
            var round = new Round();
            round.AddMatch(new Match(new Team("Team 1"), new Team("Team 2"), 0, 1));
            sut.AddRound(round);
            Assert.Equal(expected, sut.GetTable().ToString());
        }
        
        [Fact]
        public void HalfSeason_WithTwoMatches_WinDraw()
        {
            var expected = "| Team | Played | Won | Drawn | Lost | GF | GA | GD | Points |\n" +
                              "| Team 2 | 1 | 1 | 0 | 0 | 1 | 0 | 1 | 3 |\n" +
                              "| Team 3 | 1 | 0 | 1 | 0 | 1 | 1 | 0 | 1 |\n" +
                              "| Team 4 | 1 | 0 | 1 | 0 | 1 | 1 | 0 | 1 |\n" +
                              "| Team 1 | 1 | 0 | 0 | 1 | 0 | 1 | -1 | 0 |";
            var sut = new HalfSeason();
            var round = new Round();
            round.AddMatch(new Match(new Team("Team 1"), new Team("Team 2"), 0, 1));
            round.AddMatch(new Match(new Team("Team 3"), new Team("Team 4"), 1, 1));
            sut.AddRound(round);
            Assert.Equal(expected, sut.GetTable().ToString());
        }
    }
}
