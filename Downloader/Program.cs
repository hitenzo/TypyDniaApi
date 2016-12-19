using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ConfigGenerator;
using MatchDownloader.Model;
using Shared.Model;

namespace MatchDownloader
{
    public class Program
    {
        static void Main(string[] args)
        {
//            var cfgPath = "E:\\projects\\TypyDniaApi\\downloader_cfg.xml";
//            XmlDocument doc = new XmlDocument();
//            XmlTextReader reader = new XmlTextReader(cfgPath);
//            doc.Load(reader);
//            var cfg = new Config(doc);


//            string threads = args[0];
//            string url = args[1];
//            string league = args[2];
//            string years = args[3];
//            string saveDetailsPath = args[4];

            args = new[]
            {
                "3",
                "http://localhost:2710//api//WhoScored//GetMatchDetails",
                "Eredivisie",
                "2015/2016",
                "E:\\Data"
            };

            var cfgGen = new Generator();
            var cfg = cfgGen.GetConfig(args);

            var downloader = new Downloader(cfg);
            var writer = new MatchSaver(cfg);
            var manager = new Manager(downloader, writer, cfg);

            string result = manager.Perform();
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
