using System.Collections.Generic;
using System.Xml;
using Shared.Model;
using Shared.Model.Requests;

namespace ConfigGenerator
{
    public class Generator
    {
//        private string _url = "http://localhost:2710//api//WhoScored//GetMatchDetails";

//        private string _saveDetailsPath = "E://GameData";

//        private string _saveConfigPath = "E://projects//TypyDniaApi//TypyDniaApi//Configs//Myxml.xml";

        public Config GetConfig(string[] args)
        {
            string threads = args[0];

            //jeden z arguemtnow będzie mozna przelozyc na 1) season request
            //2) robisz calla po liste match requestow dla sezonu
            //3) serializujesz to na liste z jsona
            List<MatchRequest> requestsList = new List<MatchRequest>();
            XmlDocument myxml = new XmlDocument();

            XmlElement cfgTag = myxml.CreateElement("cfg");

            XmlElement threadsTag = myxml.CreateElement("threads");
            threadsTag.InnerText = threads;

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

            return new Config(myxml);
        }
    }
}