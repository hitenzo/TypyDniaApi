using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TypyDniaApi.Model.DataSource;
using TypyDniaApi.Model.Helpers;

namespace TypyDniaApi.Model.MatchObjects
{
    public class MatchDetails
    {
        public MatchDetails(string matchDate, int teamId, IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            var teamUrl = "https://www.whoscored.com/Teams/" + teamId;

            driver.Navigate().GoToUrl(teamUrl);

            IWebElement fixturesButton = wait.Until(
                ExpectedConditions.ElementIsVisible(By.CssSelector(Selectors.GetSelector("FixturesButton"))));

            fixturesButton.Click();

            IEnumerable<IWebElement> matches = wait.Until(
                ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(Selectors.GetSelector("Fixtures"))));

            DateTime reqDate = DateTime.ParseExact(matchDate, "dd-MM-yy", CultureInfo.InvariantCulture);

            IWebElement matchElement = matches.
                FirstOrDefault(x => x.FindElement(By.CssSelector("td.date")).Text == reqDate.ToString("dd-MM-yyyy"));

            if (matchElement == null)
            {
                //todo: match not found, throw exception? log it to logger?
                return;
            }

            matchElement.FindElement(By.CssSelector(Selectors.GetSelector("MatchReport"))).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(Selectors.GetSelector("Date"))));

            WhoScoredScraper.FillObject(this, driver);
            HomeTeamDetails = new Details(driver, "Home");
            AwayTeamDetails = new Details(driver, "Away");
        }

        public string Date { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public string HtScore { get; set; }

        public string FtScore { get; set; }

        public Details HomeTeamDetails { get; set; }

        public Details AwayTeamDetails { get; set; }
    }
}