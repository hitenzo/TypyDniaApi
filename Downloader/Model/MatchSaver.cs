using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.Model;
using TypyDniaApi.Model.MatchObjects;

namespace MatchDownloader.Model
{
    public class MatchSaver : ISaver
    {
        private Config _cfg;

        public MatchSaver(Config cfg)
        {
            _cfg = cfg;
        }

        public void Save(string content)
        {
            var jss = new JavaScriptSerializer();
            dynamic deserializedData = jss.Deserialize<dynamic>(content);

            dynamic matchDetailsJson = JObject.Parse(deserializedData);
            string homeTeam = matchDetailsJson["HomeTeam"].ToString();
            string awayTeam = matchDetailsJson["AwayTeam"].ToString();
            string matchDate = matchDetailsJson["Date"].ToString();

            string filePath = _cfg.MatchesSavePath + "//" + homeTeam + "-" + awayTeam + "-" + matchDate + ".txt";

            if (!Directory.Exists(_cfg.MatchesSavePath))
            {
                Directory.CreateDirectory(_cfg.MatchesSavePath);
            }

            File.WriteAllText(filePath, matchDetailsJson.ToString());
        }
    }
}
