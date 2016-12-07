using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenQA.Selenium;
using TypyDniaApi.Model.Helpers;

namespace TypyDniaApi.Model.MatchObjects
{
    public class Details
    {
        public Details(IWebDriver driver, string prefix)
        {
            Stats = new MatchStats(driver, prefix);
            Attempts = new Attempts(driver, prefix);
            Passes = new Passes(driver, prefix);
            Cards = new Cards(driver, prefix);
        }

        public MatchStats Stats { get; set; }
        public Attempts Attempts { get; set; }
        public Passes Passes { get; set; }
        public Cards Cards { get; set; }
    }
}