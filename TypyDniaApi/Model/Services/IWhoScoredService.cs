using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TypyDniaApi.Model.MatchObjects;
using TypyDniaApi.Model.Requests;

namespace TypyDniaApi.Model.Services
{
    public interface IWhoScoredService
    {
        MatchDetails GetMatchDetails(string date, int homeTeamId);
        List<MatchRequest> GetSeasonMatches(SeasonRequest request);
    }
}