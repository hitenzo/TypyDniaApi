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

            args = new[]
            {
                "pierwszy parametr zahardkodowany przez wojtka hakera",
                "drugi parametr"
            };

            var cfgGen = new Generator();
            var cfg = cfgGen.GetConfig(args);

            var downloader = new Downloader(cfg);
            var writer = new Writer(cfg);
            var manager = new Manager(downloader, writer, cfg);

            string result = manager.Perform();
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
