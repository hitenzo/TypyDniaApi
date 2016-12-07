using System;

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
            Console.WriteLine("zapisalem");
        }
    }
}
