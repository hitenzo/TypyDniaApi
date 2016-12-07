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
    public class Cards
    {
        public Cards(IWebDriver driver, string prefix)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            IWebElement passButton = wait.Until(
                            ExpectedConditions.ElementIsVisible(By.CssSelector(Selectors.GetSelector("CardSituationsButton"))));
            
            passButton.Click();
            wait.Until(
                            ExpectedConditions.ElementIsVisible(By.CssSelector(Selectors.GetSelector("CardsTotal" + prefix))));
            WhoScoredScraper.FillObject(this, driver, prefix);
        }

        public int CardsTotal { get; set; }
        public int CardsFromFaul { get; set; }
        public int CardsFromUnprofessional { get; set; }
        public int CardsFromDive { get; set; }
        public int CardsFromOther { get; set; }
        public int RedCardsPerGame { get; set; }
        public int YellowCardsPerGame { get; set; }
        public string CardsPerFaul { get; set; }
        public int FaulsPerGame { get; set; }
    }
}