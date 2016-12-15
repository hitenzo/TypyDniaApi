using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.Model;
using TypyDniaApi.Model.MatchObjects;

namespace MatchDownloader.Model
{
    public class Writer : IWriter
    {
        private Config _cfg;

        public Writer(Config cfg)
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

            string filePath = _cfg.SavePath + "//" + homeTeam + "-" + awayTeam + "-" + matchDate + ".txt";
            System.IO.File.WriteAllText(filePath, matchDetailsJson.ToString());
        }
    }
}
