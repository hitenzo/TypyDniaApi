using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TypyDniaApi.Model.Requests;

namespace MatchDownloader.Model
{
    public class Downloader
    {
        private Config _cfg;

        public Downloader(Config cfg)
        {
            _cfg = cfg;
        }

        public List<Task<string>> GetDownloadTasks()
        {
            List<MatchRequest> requestedList = _cfg.RequestedList;
            string apiUrl = _cfg.ApiUrl;

            List<Task<string>> tasksList = new List<Task<string>>();

            foreach (var matchRequest in requestedList)
            {
                Task<string> taskToDo = new Task<string>(() => PostApiCall(apiUrl, matchRequest));
                tasksList.Add(taskToDo);
            }
            return tasksList;
        }

        public string PostApiCall(string url, MatchRequest matchRequest)
        {
            string responseString = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";

            string data = JsonConvert.SerializeObject(matchRequest);
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(data);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Stream respStream = response.GetResponseStream();
                    if (respStream != null)
                    {
                        responseString = new StreamReader(respStream).ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                // Log exception and throw as for GET example above
                var hh = 6;
            }
            //            Thread.Sleep(5000);
            //            return "skonczone";
            return responseString;
        }
    }
}
