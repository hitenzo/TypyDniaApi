using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;
using TypyDniaApi.Model.MatchObjects;
using TypyDniaApi.Model.Helpers;
using TypyDniaApi.Model.Requests;

namespace TypyDniaApi.Model.DataSource
{
    public class WhoScoredScraper
    {
        public MatchDetails GetMatchDetails(string matchDate, int teamId)
        {
            IWebDriver driver = new PhantomJSDriver(@"C:\Program Files\phantomjs-2.1.1-windows\phantomjs-2.1.1-windows\bin");

            MatchDetails scrapedDetails = new MatchDetails(matchDate, teamId, driver);

            driver.Quit();

            return scrapedDetails;
        }

        public List<MatchRequest> GetSeasonMatches(SeasonRequest request)
        {
            //IWebDriver driver = new PhantomJSDriver(@"C:\Program Files\phantomjs-2.1.1-windows\phantomjs-2.1.1-windows\bin");
            IWebDriver driver = new ChromeDriver(@"C:\Users\kuite\Desktop\");

            SeasonMatches seasonMatches = new SeasonMatches(request, driver);

            driver.Quit();

            return seasonMatches.MatchRequests;
        }

        public static void WaitForPageLoad(int maxWaitTimeInSeconds, IWebDriver driver)
        {
            string state = string.Empty;
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(maxWaitTimeInSeconds));
                
                //Checks every 500 ms whether predicate returns true if returns exit otherwise keep trying till it returns ture
                wait.Until(d =>
                {
                    try
                    {
                        state = ((IJavaScriptExecutor)driver).ExecuteScript(@"return document.readyState").ToString();
                    }
                    catch (InvalidOperationException)
                    {
                        //Ignore
                    }
                    catch (NoSuchWindowException)
                    {
                        //when popup is closed, switch to last windows
                        driver.SwitchTo().Window(driver.WindowHandles.Last());
                    }
                    //In IE7 there are chances we may get state as loaded instead of complete
                    return (state.Equals("complete", StringComparison.InvariantCultureIgnoreCase) || state.Equals("loaded", StringComparison.InvariantCultureIgnoreCase));
                });
                //wait.Until(d => true);
            }
            catch (TimeoutException)
            {
                //sometimes Page remains in Interactive mode and never becomes Complete, then we can still try to access the controls
                if (!state.Equals("interactive", StringComparison.InvariantCultureIgnoreCase))
                    throw;
            }
            catch (NullReferenceException)
            {
                //sometimes Page remains in Interactive mode and never becomes Complete, then we can still try to access the controls
                if (!state.Equals("interactive", StringComparison.InvariantCultureIgnoreCase))
                    throw;
            }
            catch (WebDriverException)
            {
                if (driver.WindowHandles.Count == 1)
                {
                    driver.SwitchTo().Window(driver.WindowHandles[0]);
                }
                state = ((IJavaScriptExecutor)driver).ExecuteScript(@"return document.readyState").ToString();
                if (!(state.Equals("complete", StringComparison.InvariantCultureIgnoreCase) || state.Equals("loaded", StringComparison.InvariantCultureIgnoreCase)))
                    throw;
            }
        }

        public static void FillObject(object instance, IWebDriver driver, string prefix = null)
        {
            foreach (PropertyInfo info in instance.GetType().GetProperties())
            {
                var propType = info.PropertyType;
                if (propType == typeof(String) || propType.IsPrimitive)
                {
                    string value = GetValueFromWebElement(driver, info.Name + prefix);

                    info.SetValue(instance, Convert.ChangeType(value, info.PropertyType), null);
                }
            }
        }


        private static string GetValueFromWebElement(IWebDriver driver, string propertyName)
        {
            try
            {
                var element = driver.FindElement(By.CssSelector(Selectors.GetSelector(propertyName)));

                if (element.Text == "")
                {
                    return "-1";
                }

                return element.Text;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}