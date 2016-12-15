using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Shared.Model.Requests;
using TypyDniaApi.Model.MatchObjects;
using TypyDniaApi.Model.Repostiories;


namespace TypyDniaApi.Model.Services
{
    public class WhoScoredService : IWhoScoredService
    {
        private IMatchDetailsRepository _repository;

        public WhoScoredService(IMatchDetailsRepository repository)
        {
            _repository = repository;
        }

        public MatchDetails GetMatchDetails(string date, int homeTeamId)
        {
            return _repository.GetMatchDetails(date, homeTeamId);
        }

        public List<MatchRequest> GetSeasonMatches(SeasonRequest request)
        {
            return _repository.GetSeasonMatches(request);
        }
    }
}