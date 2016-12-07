using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using TypyDniaApi.Model.Helpers;

namespace TypyDniaApi.Model.ForumObjects
{
    public class Prediction
    {
        public DateTime EventDate { get; set; }

        public string IsWon { get; set; }

        public string Title { get; set; }

        public double Odds { get; set; }

        public string Branch { get; set; }

        public string Analyse { get; set; }

        public Prediction(HtmlNode postNode, DateTime date)
        {
            Title = "just grab another pawn!";
            IsWon = "not decided";
            EventDate = GetEventDate(postNode, date);
            Odds = GetOdds(postNode);
            Branch = GetBranch(postNode);
            Analyse = GetAnalyse(postNode);
        }

        public static bool IsPostReadable(HtmlNode postNode)
        {
            bool canReadPredict = true;



            return canReadPredict;
        }

        private string GetAnalyse(HtmlNode postNode)
        {
            string content = Utils.GetPostContent(postNode);
            int startIndex = content.IndexOf("Analiza:");
            string analise = content.Substring(startIndex + 8).Trim();

            return analise;
        }

        private string GetBranch(HtmlNode postNode)
        {
            var branch = string.Empty;
            List<string> content = Utils.GetPostAsList(postNode);

            foreach (var singleLine in content)
            {
                if (singleLine.Contains("Dyscyplina") || singleLine.Contains("dyscyplina"))
                {
                    var branchIndex = singleLine.IndexOf("Dyscyplina", StringComparison.CurrentCultureIgnoreCase);
                    var branchLine = singleLine.Substring(branchIndex);
                    branchLine = branchLine.Remove(0, 10);
                    branch = branchLine.Replace(":", "").Trim();
                }
            }

            return branch;
        }

        private double GetOdds(HtmlNode postNode)
        {
            double odds = 0;
            List<string> content = Utils.GetPostAsList(postNode);

            foreach (var singleLine in content)
            {
                if (singleLine.StartsWith("Kurs", StringComparison.CurrentCultureIgnoreCase))
                {
                    var oddsLine = singleLine.Remove(0, 4);
                    oddsLine = oddsLine.Replace(":", "").Replace(" ", "").Replace(",", ".");
                    try
                    {
                        odds = double.Parse(oddsLine, CultureInfo.InvariantCulture);
                    }
                    catch (Exception e)
                    {
                        odds = 0;
                    }
                }
            }

            return odds;
        }

        private DateTime GetEventDate(HtmlNode postNode, DateTime date)
        {
            DateTime eventDate = date;
            string eventTime = string.Empty;
            List<string> content = Utils.GetPostAsList(postNode);

            foreach (var singleLine in content)
            {
                if (singleLine.StartsWith("Godzina", StringComparison.CurrentCultureIgnoreCase))
                {
                    eventTime = singleLine.Remove(0, 7);
                }
                else if (singleLine.StartsWith("Godz", StringComparison.CurrentCultureIgnoreCase))
                {
                    eventTime = singleLine.Remove(0, 4);
                }
            }

            if (eventTime.Length > 0)
            {
                eventTime = eventTime.Replace(":", "").Replace(" ", "");
                try
                {
                    var eventHour = int.Parse(eventTime.Substring(0, 2));
                    var eventMinute = int.Parse(eventTime.Substring(2));
                    eventDate = new DateTime(date.Year, date.Month, date.Day, eventHour, eventMinute, 0);
                }
                catch (Exception e)
                {
                    eventDate = date;
                }
            }

            return eventDate;
        }
    }
}