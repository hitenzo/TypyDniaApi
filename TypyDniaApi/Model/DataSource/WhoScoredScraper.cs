using System;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using TypyDniaApi.Model.MatchObjects;
using TypyDniaApi.Model.Helpers;

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