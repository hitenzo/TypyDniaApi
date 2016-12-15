using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shared.Model.Requests;
using TypyDniaApi.Model.MatchObjects;

namespace TypyDniaApi.Model.Services
{
    public interface IWhoScoredService
    {
        MatchDetails GetMatchDetails(string date, int homeTeamId);
        List<MatchRequest> GetSeasonMatches(SeasonRequest request);
    }
}