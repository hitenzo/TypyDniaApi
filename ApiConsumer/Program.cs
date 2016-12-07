using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TypyDniaApi.Model.MatchObjects;
using TypyDniaApi.Model.Requests;

namespace ApiConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:2710//api//WhoScored//GetMatchDetails";

            MatchRequest request = new MatchRequest();
            request.HomeTeamId = 75;
            request.Date = "03-11-16";

            MatchRequest request2 = new MatchRequest();
            request.HomeTeamId = 256;
            request.Date = "06-11-16";

            MatchRequest request3 = new MatchRequest();
            request.HomeTeamId = 36;
            request.Date = "18-11-16";
            string x = "";
            Task.Factory.StartNew(() => x = PostApiCall(url, request));
            Task.Factory.StartNew(() => PostApiCall(url, request2));
            Task.Factory.StartNew(() => PostApiCall(url, request3));

            Console.WriteLine("lecimy tutaj");
            Console.ReadKey();
        }

        private static string PostApiCall(string url, MatchRequest matchRequest)
        {
            string responseString = string.Empty;
//            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
//            request.Method = "POST";
//
//            string data = JsonConvert.SerializeObject(matchRequest);
//            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
//            Byte[] byteArray = encoding.GetBytes(data);
//
//            request.ContentLength = byteArray.Length;
//            request.ContentType = @"application/json";
//
//            using (Stream dataStream = request.GetRequestStream())
//            {
//                dataStream.Write(byteArray, 0, byteArray.Length);
//            }
//            try
//            {
//                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
//                {
//                    Stream respStream = response.GetResponseStream();
//                    if (respStream != null)
//                    {
//                        responseString = new StreamReader(respStream).ReadToEnd();
//                    }
//                }
//            }
//            catch (WebException ex)
//            {
//                // Log exception and throw as for GET example above
//                var hh = 6;
//            }
            Thread.Sleep(5000);
            return "skonczone";
            return responseString;
        }
    }
}
