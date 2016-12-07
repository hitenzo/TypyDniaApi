using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TypyDniaApi.Model.DataSource;
using TypyDniaApi.Model.Helpers;

namespace TypyDniaApi.Model.MatchObjects
{
    public class Passes
    {
        public Passes(IWebDriver driver, string prefix)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            IWebElement passButton = wait.Until(
                            ExpectedConditions.ElementIsVisible(By.CssSelector(Selectors.GetSelector("PassTypesButton"))));

            passButton.Click();
            wait.Until(
                            ExpectedConditions.ElementIsVisible(By.CssSelector(Selectors.GetSelector("PassesTotal" + prefix))));
            WhoScoredScraper.FillObject(this, driver, prefix);
        }

        public int PassesTotal { get; set; }
        public int Crosses { get; set; }
        public int ThroughBalls { get; set; }
        public int LongBalls { get; set; }
        public int AveragePassStreak { get; set; }
    }
}