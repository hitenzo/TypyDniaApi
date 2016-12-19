using System.Collections.Generic;
using System.Xml;
using Newtonsoft.Json;
using Shared.Model;
using Shared.Model.Requests;
using TypyDniaApi.Controllers;
using TypyDniaApi.Model.DataSource;
using TypyDniaApi.Model.Repostiories;
using TypyDniaApi.Model.Services;

namespace ConfigGenerator
{
    public class Generator
    {
        private readonly WhoScoredController _whoScoredController;

        public Generator()
        {
            var scraper = new WhoScoredScraper();
            var whoScoredRepository = new MatchDetailsRepository(scraper);
            var whoScoredService = new WhoScoredService(whoScoredRepository);
            _whoScoredController = new WhoScoredController(whoScoredService);
        }

        public Config GetConfig(string[] args)
        {
            var seasonRequest = new SeasonRequest();

            string threads = args[0];
            string url = args[1];
            string league = args[2];
            string years = args[3];
            string saveDetailsPath = args[4];

            seasonRequest.League = league;
            seasonRequest.Years = years;

            saveDetailsPath = string.Concat(saveDetailsPath, "\\", league, "-", years);

            string allMatchesRequest = _whoScoredController.GetSeasonMatches(seasonRequest);

            List<MatchRequest> requestsList = JsonConvert.DeserializeObject<List<MatchRequest>>(allMatchesRequest);

            XmlDocument myxml = new XmlDocument();
            XmlElement cfgTag = myxml.CreateElement("cfg");
            XmlElement threadsTag = myxml.CreateElement("threads");
            threadsTag.InnerText = threads;

            XmlElement urlTag = myxml.CreateElement("url");
            urlTag.InnerText = url;

            XmlElement saveDetailsTag = myxml.CreateElement("save-path");
            saveDetailsTag.InnerText = saveDetailsPath;

            XmlElement requestsTag = myxml.CreateElement("requests");

            foreach (var singleRequest in requestsList)
            {
                XmlElement singleRequestTag = myxml.CreateElement("request");
                singleRequestTag.SetAttribute("home-id", singleRequest.HomeTeamId.ToString());
                singleRequestTag.SetAttribute("date", singleRequest.Date);
                requestsTag.AppendChild(singleRequestTag);
            }

            cfgTag.AppendChild(threadsTag);
            cfgTag.AppendChild(urlTag);
            cfgTag.AppendChild(saveDetailsTag);
            cfgTag.AppendChild(requestsTag);
            myxml.AppendChild(cfgTag);

            return new Config(myxml);
        }
    }
}