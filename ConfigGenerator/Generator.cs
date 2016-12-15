using System.Collections.Generic;
using System.Xml;

namespace ConfigGenerator
{
    public class Generator
    {
        private int _threads = 4;

        private string _url = "http://localhost:2710//api//WhoScored//GetMatchDetails";

        private string _saveDetailsPath = "E://GameData";

        private string _saveConfigPath = "E://projects//TypyDniaApi//TypyDniaApi//Configs//Myxml.xml";



        public void GenerateConfig(List<MatchRequest> requestsList)
        {
            XmlDocument myxml = new XmlDocument();

            XmlElement cfgTag = myxml.CreateElement("cfg");

            XmlElement threadsTag = myxml.CreateElement("threads");
            threadsTag.InnerText = _threads.ToString();

            XmlElement urlTag = myxml.CreateElement("url");
            urlTag.InnerText = _url;

            XmlElement saveDetailsTag = myxml.CreateElement("save-path");
            saveDetailsTag.InnerText = _saveDetailsPath;

            XmlElement requestsTag = myxml.CreateElement("requests");

            foreach (var singleRequest in requestsList)
            {
                XmlElement singleRequestTag = myxml.CreateElement("request");
                singleRequestTag.SetAttribute("home-id", singleRequest.HomeTeamId.ToString());
                singleRequestTag.SetAttribute("date", singleRequest.Date);
                requestsTag.AppendChild(singleRequestTag);
            }

            cfgTag.AppendChild(threadsTag);

            cfgTag.AppendChild(urlTag);

            cfgTag.AppendChild(saveDetailsTag);

            cfgTag.AppendChild(requestsTag);

            myxml.AppendChild(cfgTag);

            myxml.Save(_saveConfigPath);
        }
    }
}