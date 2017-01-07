using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TypyDniaApi.Model.ForumObjects;
using TypyDniaApi.Model.MatchObjects;

namespace TypyDniaApi.Model.Helpers
{
    public static class Utils
    {
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

        public static string FormatJson(string inputText)
        {
            string replaced = inputText.Replace(@"\r\n\t\t\t\", Environment.NewLine);

            return replaced;
        }

        public static string GetLogin(HtmlNode postNode)
        {
            var login = string.Empty;
            HtmlNode loginDiv = postNode.Descendants("div").FirstOrDefault();
            if (loginDiv != null)
            {
                login = loginDiv.InnerText;
            }

            if (login.First().Equals(' '))
            {
                login = login.Remove(0, 1);
            }
            if (login.Last().Equals(' '))
            {
                login = login.Remove(login.Length - 1, 1);
            }
            return login;
        }

        public static int GetPostCount(HtmlNode postNode)
        {
            int count = 0;

            HtmlNode detailsDiv =
                postNode.Descendants()
                    .FirstOrDefault(
                    desc => desc.Name.Equals("div") &&
                    desc.InnerText.Contains("Postów:"));

            if (detailsDiv != null)
            {
                HtmlNode postsDiv =
                    detailsDiv.Descendants().FirstOrDefault(desc => desc.InnerText.Contains("Postów:"));

                if (postsDiv != null)
                {
                    var countStr = postsDiv.InnerHtml.Replace(",", "");

                    countStr = new string(countStr
                        .SkipWhile(x => !char.IsDigit(x))
                        .TakeWhile(x => char.IsDigit(x))
                        .ToArray());

                    Int32.TryParse(countStr, out count);
                }
            }

            return count;
        }

        public static string GetPostContent(HtmlNode postNode)
        {
            var content = "do not parsed post content";

            HtmlNode postsDiv = postNode.Descendants().FirstOrDefault(
                    desc => desc.Name.Equals("div") &&
                    desc.Attributes["id"] != null &&
                    desc.Attributes["id"].Value.Contains("post_message"));

            if (postsDiv != null)
            {
                content = postsDiv.InnerText.Remove(0, 8);
            }


            return content;
        }

        public static List<string> GetPostAsList(HtmlNode postNode)
        {
            var content = "do not parsed post content";
            List<string> contentList = new List<string>();

            HtmlNode postsDiv = postNode.Descendants().FirstOrDefault(
                    desc => desc.Name.Equals("div") &&
                    desc.Attributes["id"] != null &&
                    desc.Attributes["id"].Value.Contains("post_message"));

            if (postsDiv != null)
            {
                content = postsDiv.InnerHtml;
                content = content.Replace("<b>", "").Replace("</b>", "").Replace("\"", "").Replace("<i>", "").Replace("</i>", "");
                List<string> contentLines = content.Split(new string[] { "<br>" }, StringSplitOptions.None).ToList();

                foreach (var contentItem in contentLines)
                {
                    contentList.Add(contentItem.Trim());
                }
            }
            return contentList;
        }

        public static Prediction GetPredictionFromPost(HtmlNode postNode, DateTime date)
        {
            Prediction pred = null;
            if (Prediction.IsPostReadable(postNode))
            {
                pred = new Prediction(postNode, date);
            }
            return pred;
        }

        public static DateTime GetJoinedDate(HtmlNode postNode)
        {
            //todo wojtek

            var day = 0;
            var month = 0;
            var year = 0;

            HtmlNode detailsDiv =
                postNode.Descendants()
                    .FirstOrDefault(
                    desc => desc.Name.Equals("div") &&
                    desc.InnerText.Contains("Dołączył:"));

            if (detailsDiv != null)
            {
                HtmlNode joinedDiv =
                    detailsDiv.Descendants().FirstOrDefault(desc => desc.InnerText.Contains("Dołączył:"));

                if (joinedDiv != null)
                {
                    var innerStr = joinedDiv.InnerHtml.Trim();
                    var strLength = innerStr.Length;
                    day = int.Parse(innerStr.Substring(strLength - 10, 2));
                    month = int.Parse(innerStr.Substring(strLength - 7, 2));
                    year = int.Parse(innerStr.Substring(strLength - 4, 4));
                }
            }

            return new DateTime(year, month, day);
        }

        public static DateTime GetPostDate(HtmlNode postNode)
        {
            //todo
            DateTime postDate = new DateTime();

            HtmlNode detailsTd =
                postNode.Descendants()
                    .FirstOrDefault(
                    desc => desc.Name.Equals("td") &&
                    desc.Attributes["class"].Value == "thead");

            if (detailsTd != null)
            {
                var postDateInfo = detailsTd.InnerText.Trim();

                var postHours = int.Parse(postDateInfo.Substring(postDateInfo.Length - 5, 2));
                var postMinutes = int.Parse(postDateInfo.Substring(postDateInfo.Length - 2, 2));
                TimeSpan time = new TimeSpan(postHours, postMinutes, 0);

                if (postDateInfo.StartsWith("Wczoraj"))
                {
                    var yestarday = DateTime.Today.AddDays(-1);
                    postDate = yestarday.Date + time;
                }
                else if (postDateInfo.StartsWith("Dzisiaj"))
                {
                    var today = DateTime.Today;
                    postDate = today.Date + time;
                }
                else
                {
                    var day = int.Parse(postDateInfo.Substring(0, 2));
                    var month = int.Parse(postDateInfo.Substring(3, 2));
                    var year = int.Parse(postDateInfo.Substring(6, 4));

                    postDate = new DateTime(year, month, day, postHours, postMinutes, 0);
                }
            }

            return postDate;
        }

        private static List<string> ExtractFromString(string text, string startString, string endString)
        {
            List<string> matched = new List<string>();
            int indexStart = 0, indexEnd = 0;
            bool exit = false;
            while (!exit)
            {
                indexStart = text.IndexOf(startString);
                indexEnd = text.IndexOf(endString);
                if (indexStart != -1 && indexEnd != -1)
                {
                    matched.Add(text.Substring(indexStart + startString.Length,
                        indexEnd - indexStart - startString.Length));
                    text = text.Substring(indexEnd + endString.Length);
                }
                else
                    exit = true;
            }
            return matched;
        }

        private static List<int> AllIndexesOf(string str, string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("the string to find may not be empty", "value");
            }
            List<int> indexes = new List<int>();
            for (int index = 0; ;index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                {
                    return indexes;
                }
                indexes.Add(index);
            }
        }
    }
}