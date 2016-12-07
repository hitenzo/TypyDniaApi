using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypyDniaApi.Model.MatchObjects;
using TypyDniaApi.Model.ForumObjects;
using TypyDniaApi.Model.Requests;

namespace TypyDniaApi.Model.Repostiories
{
    public interface IMatchDetailsRepository
    {
        MatchDetails GetMatchDetails(string date, int teamId);
        List<MatchRequest> GetSeasonMatches(SeasonRequest request);
    }
}
