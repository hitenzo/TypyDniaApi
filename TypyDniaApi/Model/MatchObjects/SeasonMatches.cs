using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Web;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using Shared.Model.Requests;
using TypyDniaApi.Model.DataSource;
using TypyDniaApi.Model.Helpers;

namespace TypyDniaApi.Model.MatchObjects
{
    public class SeasonMatches
    {
        public List<MatchRequest> MatchRequests { get; set; }

        public SeasonMatches(SeasonRequest request, IWebDriver driver)
        {
            MatchRequests = new List<MatchRequest>();
            string years = request.Years;
            string league = request.League;

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var startingUrl = "https://www.whoscored.com/";
            driver.Navigate().GoToUrl(startingUrl);

            IWebElement leagueButton = driver.FindElement(By.CssSelector(Selectors.GetSelector("DetailedTournamentsButton")));
            IEnumerable<IWebElement> leagueList = driver.FindElements(By.CssSelector(Selectors.GetSelector("LeagueList")));

            if (leagueList.Any() && !leagueList.First().Displayed)
            {
                leagueButton.Click();
            }

            IWebElement requestedLeague = leagueList.FirstOrDefault(x => x.Text == league);
            if (requestedLeague != null)
            {
                requestedLeague.Click();
            }

            IWebElement seasonDropDown = driver.FindElement(By.CssSelector(Selectors.GetSelector("YearsList")));
            IEnumerable<IWebElement> seasonsList = seasonDropDown.FindElements(By.CssSelector("option"));

            IWebElement requestedYears = seasonsList.FirstOrDefault(x => x.Text == years);
            if (requestedYears != null)
            {
                requestedYears.Click();
            }

            IWebElement datePicker = wait.Until(x => driver.FindElement(By.CssSelector(Selectors.GetSelector("DatePicker"))));

            IWebElement datePickerButton = wait.Until(x => driver.FindElement(By.CssSelector(Selectors.GetSelector("DatePickerButton"))));

            if (!datePicker.Displayed)
            {
                datePickerButton.Click();
            }

            IWebElement firstYear = wait.Until(
                ExpectedConditions.ElementIsVisible(By.CssSelector(Selectors.GetSelector("FirstYear"))));

            if (!firstYear.GetAttribute("class").Split(' ').Contains("selected"))
            {
                firstYear.Click();
            }

            IEnumerable<IWebElement> activeMonths = driver.FindElements(By.CssSelector(Selectors.GetSelector("ActiveMonths")));
            activeMonths.First().Click();

            IEnumerable<IWebElement> activeWeeks = driver.FindElements(By.CssSelector(Selectors.GetSelector("ActiveWeeks")));
            activeWeeks.First().Click();

            ScrapeSeasonMatches(driver);
        }

        private void ScrapeSeasonMatches(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(7));
            string matchDate = string.Empty;

            while (true)
            {
                IEnumerable<IWebElement> matchesInDetails = wait.Until(x => x.FindElements(By.CssSelector(Selectors.GetSelector("MatchesInDetails"))));

                foreach (var singleMatch in matchesInDetails)
                {
                    if (singleMatch.GetAttribute("class").Contains("rowgroup"))
                    {
                        IWebElement dateHolder = wait.Until(x => singleMatch.FindElement(By.CssSelector("th")));
                        matchDate = dateHolder.Text;
                    }
                    else
                    {
                        if (singleMatch.Text.Contains("Preview"))
                        {
                            break;
                        }

                        var homeTeamIdString = singleMatch.FindElement(By.CssSelector("td.home"))
                            .GetAttribute("data-id");
                        var homeTeamId = int.Parse(homeTeamIdString);

                        var matchRequest = new MatchRequest();
                        matchRequest.HomeTeamId = homeTeamId;
                        matchRequest.Date = matchDate;

                        MatchRequests.Add(matchRequest);
                    }
                }

                try
                {
                    wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(Selectors.GetSelector("NextWeekButton"))))
                        .GetAttribute("title")
                        .Contains("View");
                }
                catch (WebDriverTimeoutException ex)
                {
                    break;
                    //var err = driver.TakeScreenshot();
                    //err.SaveAsFile(@"C:\Users\kuite\Desktop\error.jpg", ImageFormat.Jpeg);
                }

                string firstMatchHtml = matchesInDetails.FirstOrDefault().GetAttribute("innerHTML");
                driver.FindElement(By.CssSelector(Selectors.GetSelector("NextWeekButton"))).Click();
                
                bool isNotChanged = true;

                while (isNotChanged)
                {
                    try
                    {
                        IWebElement newFirstMatch = wait.Until(x => x.FindElements(By.CssSelector(Selectors.GetSelector("MatchesInDetails")))
                        .FirstOrDefault());

                        string newFirstMatchHtml = wait.Until(x => newFirstMatch.GetAttribute("innerHTML"));
                        if (firstMatchHtml != newFirstMatchHtml)
                        {
                            isNotChanged = false;
                        }
                    }
                    catch (StaleElementReferenceException ex)
                    {
                        //do nothing
                    }
                }
                
            }
        }
    }
}