﻿using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Web;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using TypyDniaApi.Model.DataSource;
using TypyDniaApi.Model.Helpers;
using TypyDniaApi.Model.Requests;

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
                Thread.Sleep(500);
                IEnumerable<IWebElement> matchesInDetails = wait.Until(x => x.FindElements(By.CssSelector(Selectors.GetSelector("MatchesInDetails"))));

                foreach (var singleMatch in matchesInDetails)
                {
                    if (singleMatch.GetAttribute("class").Contains("rowgroup")) // if (singleMatch.GetAttribute("class").Contains("rowgroup"))
                    {
                        IWebElement dateHolder = wait.Until(x => singleMatch.FindElement(By.CssSelector("th")));
                        matchDate = dateHolder.Text;
                    }
                    else
                    {
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

                driver.FindElement(By.CssSelector(Selectors.GetSelector("NextWeekButton"))).Click();
            }
        }
    }
}