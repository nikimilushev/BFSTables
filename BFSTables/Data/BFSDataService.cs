using BFSDomain;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BFSTables.Data
{
    public class BFSDataService
    {
        private static Dictionary<string, int> GroupsByUrlSegment = new Dictionary<string, int>
        {
            { "2009A", 225 },
            { "2009B", 226 },
            { "2009V", 228 },
            { "2009G", 229 },

            { "2010A", 231 },
            { "2010B", 232 },
            { "2010V", 233 },
            { "2010G", 234 },

            { "2011A", 235 },
            { "2011B", 236 },
            { "2011V", 237 },
            { "2011G", 238 },

            { "2012A", 245 },
            { "2012B", 240 },
            { "2012V", 241 },
            { "2012G", 242 },

            { "2013A", 243 },
            { "2013B", 244 },
        };
        const string urlFormat = "http://zs-sofia.com/football_result/football_result/program/{0}/custom/round/";

        public Table GetData(string groupId)
        {
            var season = new HalfSeason();
            var url = string.Format(urlFormat, GroupsByUrlSegment[groupId]);

            for (int i = 1; i <= 11; i++)
            {
                var round = new Round();

                var web = new HtmlWeb();
                var doc = web.LoadFromWebAsync(url + i.ToString(), Encoding.UTF8).GetAwaiter().GetResult();
                var rows = doc.DocumentNode.SelectNodes("//table/tbody/tr");
                if (rows != null)
                {
                    foreach (var row in rows)
                    {
                        var tds = row.ChildNodes.Where(x => x.Name == "td").Select(x => x.FirstChild).Where(x => x != null).Select(x => x.InnerText).ToList();
                        if (tds.Count < 3) continue;

                        var home = tds[0];
                        var away = tds[2];

                        var score = tds[1].Replace("\t", "").Replace("\r\n", "").Replace(" ", "").Replace("(Сл.)", "");
                        var homeGoals = score.Split("-").First();
                        var awayGoals = score.Split("-").Last();

                        if (int.TryParse(homeGoals, out var hg) && int.TryParse(awayGoals, out var ag))
                        {
                            round.AddMatch(new Match(new Team(home), new Team(away), hg, ag));
                        }
                    }
                    season.AddRound(round);
                }

                Thread.Sleep(100);
            }

            return season.GetTable();
        }
    }
}
