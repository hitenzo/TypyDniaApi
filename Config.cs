using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypyDniaApi.Model.Requests;

namespace MatchDownloader.Model
{
    public class Config
    {
        public string ApiUrl { get; set; }

        public int Threads { get; set; }

        public string SavePath { get; set; }

        public List<MatchRequest> RequestedList { get; set; }

        public Config(string cfgPath)
        {
            RequestedList = new List<MatchRequest>();
            //parsing cfg
            XmlDocument doc = new XmlDocument();
            XmlTextReader reader = new XmlTextReader(cfgPath);
            doc.Load(reader);

            XmlNode apiUrlNode = doc.GetElementsByTagName("url")[0];
            ApiUrl = apiUrlNode.InnerText;

            XmlNode savePathNode = doc.GetElementsByTagName("save-path")[0];
            SavePath = savePathNode.InnerText;

            XmlNodeList tasksNumber = doc.GetElementsByTagName("threads");
            int number;
            bool result = int.TryParse(tasksNumber[0].InnerText, out number);
            if (result)
            {
                Threads = number;
            }
            else
            {
                Threads = 1;
            }

            XmlNodeList matchRequests = doc.GetElementsByTagName("request");

            foreach (XmlElement singleMatch in matchRequests)
            {
                var homeIdString = string.Empty;
                var dateString = string.Empty;
                var dataRequest = new MatchRequest();
                if (singleMatch.Attributes["home-id"] != null)
                {
                    homeIdString = singleMatch.Attributes["home-id"].Value;
                }
                if (singleMatch.Attributes["date"] != null)
                {
                    dateString = singleMatch.Attributes["date"].Value;
                }

                int homeId;
                bool parsed = int.TryParse(homeIdString, out homeId);
                if (parsed)
                {
                    dataRequest.HomeTeamId = homeId;
                    dataRequest.Date = dateString;

                    RequestedList.Add(dataRequest);
                }
            }
        }
    }
}
