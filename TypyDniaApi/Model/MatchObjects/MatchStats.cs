using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenQA.Selenium;
using TypyDniaApi.Model.DataSource;
using TypyDniaApi.Model.Helpers;

namespace TypyDniaApi.Model.MatchObjects
{
    public class MatchStats
    {
        public MatchStats(IWebDriver driver, string prefix)
        {
            WhoScoredScraper.FillObject(this, driver, prefix);
        }

        public int Shots { get; set; }
        public int ShotsOnTarget { get; set; }
        public string PassSuccess { get; set; }
        public string AerialDuelSuccess { get; set; }
        public int DribblesWon { get; set; }
        public int Tackles { get; set; }
        public string Possession { get; set; }
    }
}