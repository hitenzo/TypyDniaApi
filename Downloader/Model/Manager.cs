using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Shared.Model;

namespace MatchDownloader.Model
{
    public class Manager
    {
        private Config _cfg;
        private Downloader _downloader;
        private MatchSaver _matchSaver;

        public Manager(Downloader downloader, MatchSaver matchSaver, Config cfg)
        {
            _downloader = downloader;
            _matchSaver = matchSaver;
            _cfg = cfg;
        }

        public string Perform()
        {
            List<Task<string>> taskList = _downloader.GetDownloadTasks();
            List<string> results = new List<string>();

            int numTasks = _cfg.Threads;
            SemaphoreSlim semaphore = new SemaphoreSlim(numTasks);

            foreach (var singleTask in taskList)
            {
                semaphore.Wait();
                Task.Run(() =>
                    {
                        singleTask.Start();
                        results.Add(singleTask.Result);
                    })
                    .ContinueWith(_ => semaphore.Release());
            }
            Task.WaitAll(taskList.ToArray());
            foreach (string result in results)
            {
                _matchSaver.Save(result);
            }

            return "Pobrano 0/0";
        }
    }

}
