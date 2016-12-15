using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TypyDniaApi.Model.DataSource;
using TypyDniaApi.Model.MatchObjects;
using TypyDniaApi.Model.ForumObjects;
using TypyDniaApi.Model.Requests;

namespace TypyDniaApi.Model.Repostiories
{
    public class MatchDetailsRepository : IMatchDetailsRepository
    {
        private WhoScoredScraper _scraper;

        public MatchDetailsRepository(WhoScoredScraper scraper)
        {
            _scraper = scraper;
        }

        public MatchDetails GetMatchDetails(string date, int teamId)
        {
            MatchDetails matchDetails = _scraper.GetMatchDetails(date, teamId);

            return matchDetails;
        }

        public List<MatchRequest> GetSeasonMatches(SeasonRequest request)
        {
            List<MatchRequest> seasonMatches= _scraper.GetSeasonMatches(request);

            return seasonMatches;
        }
    }
}