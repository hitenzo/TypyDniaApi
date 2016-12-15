using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Newtonsoft.Json;
using TypyDniaApi.Model.DataSource;
using TypyDniaApi.Model.MatchObjects;
using TypyDniaApi.Model.ForumObjects;
using TypyDniaApi.Model.Repostiories;
using TypyDniaApi.Model.Requests;
using TypyDniaApi.Model.Services;

namespace TypyDniaApi.Controllers
{
    public class WhoScoredController : ApiController
    {
        private readonly IWhoScoredService _whoscoredService;

        public WhoScoredController(IWhoScoredService whoscoredService)
        {
            _whoscoredService = whoscoredService;
        }

        [HttpPost]
        public string GetMatchDetails(MatchRequest request)
        {
            //https://www.whoscored.com/Teams/256 (Feyenord) id=256

            MatchDetails details = _whoscoredService.GetMatchDetails(request.Date, request.HomeTeamId);

            return JsonConvert.SerializeObject(details);
        }

        [HttpPost]
        public string GetSeasonMatches(SeasonRequest request)
        {
            //https://www.whoscored.com/Regions/252/Tournaments/2/Seasons/5826

            List<MatchRequest> matchList = _whoscoredService.GetSeasonMatches(request);

            return JsonConvert.SerializeObject(matchList);
        }

        [HttpPost]
        public string GetTeamAllMatchesDetails(List<int> teamIds)
        {
            //todo: here we return all matches details available for team of given id
            //https://www.whoscored.com/Teams/256 (Feyenord) id=256

            throw new NotImplementedException();
        }

    }
}
