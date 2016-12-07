using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchDownloader.Model;

namespace MatchDownloader
{
    public class Program
    {
        static void Main(string[] args)
        {
            var cfg = new Config("E:\\projects\\TypyDniaApi\\downloader_cfg.xml");
            var downloader = new Downloader(cfg);
            var writer = new Writer(cfg);
            var manager = new Manager(downloader, writer, cfg);

            string result = manager.Perform();
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
