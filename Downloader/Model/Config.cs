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
        public string ApiUrl { get; private set; }

        public int Threads { get; private set; }

        public string SavePath { get; private set; }

        public List<MatchRequest> RequestedList { get; private set; }

        public Config(string cfgPath)
        {
            RequestedList = new List<MatchRequest>();
            //parsing cfg
            //to load from xml: ApiUrl and SavePath
            XmlDocument doc = new XmlDocument();
            XmlTextReader reader = new XmlTextReader(cfgPath);
            doc.Load(reader);

            XmlNodeList tasksNumber = doc.GetElementsByTagName("threads");
            int number;
            bool result = int.TryParse(tasksNumber[0].InnerText, out number);
            if (result)
            {
                Threads = number;
            }
            else
            {
                Threads = 0;
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
