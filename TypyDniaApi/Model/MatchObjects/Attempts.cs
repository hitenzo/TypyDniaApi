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
    public class Attempts
    {
        public Attempts(IWebDriver driver, string prefix)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            IWebElement passButton = wait.Until(
                            ExpectedConditions.ElementIsVisible(By.CssSelector(Selectors.GetSelector("AttemptTypesButton"))));

            passButton.Click();
            wait.Until(
                            ExpectedConditions.ElementIsVisible(By.CssSelector(Selectors.GetSelector("OpenPlay" + prefix))));
            WhoScoredScraper.FillObject(this, driver, prefix);
        }

        public int OpenPlay { get; set; }
        public int SetPiece { get; set; }
        public int CounterAttack { get; set; }
        public int Penalty { get; set; }
        public int OwnGoal { get; set; }
        public string ConversionRate { get; set; }
    }
}