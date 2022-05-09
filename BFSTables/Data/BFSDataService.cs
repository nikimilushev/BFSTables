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
        private static Dictionary<string, (int,int)> GroupsByUrlSegment = new Dictionary<string, (int,int)>
        {
            { "2009A", (225, 251) },
            { "2009B", (226, 252) },
            { "2009V", (228, 253) },
            { "2009G", (229, 254) },

            { "2010A", (231, 255) },
            { "2010B", (232, 256) },
            { "2010V", (233, 257) },
            { "2010G", (234, 258) },

            { "2011A", (235, 259) },
            { "2011B", (236, 260) },
            { "2011V", (237, 261) },
            { "2011G", (238, 262) },

            { "2012A", (245, 263) },
            { "2012B", (240, 264) },
            { "2012V", (241, 265) },
            { "2012G", (242, 266) },

            { "2013A", (243, 267) },
            { "2013B", (244, 268) },
        };
        const string urlFormat = "http://zs-sofia.com/football_result/football_result/program/{0}/custom/round/";

        public async Task<Table> GetDataAsync(string groupId)
        {
            var season = new HalfSeason();
            var urlSegments = GroupsByUrlSegment[groupId];
            var urls = new[] { urlSegments.Item1, urlSegments.Item2 }.Select(x => string.Format(urlFormat, x));
            foreach (var url in urls)
            {
                for (int i = 1; i <= 11; i++)
                {
                    var round = new Round();

                    var web = new HtmlWeb();
                    var doc = await web.LoadFromWebAsync(url + i.ToString(), Encoding.UTF8);
                    
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

                    //Thread.Sleep(100);
                }
            }

            return season.GetTable();
        }
    }
}
