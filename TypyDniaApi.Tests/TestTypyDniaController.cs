using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using TypyDniaApi.Controllers;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;


namespace TypyDniaApi.Tests
{
    [TestFixture]
    public class TestTypyDniaController
    {
        //w stupie wszystkie wspodzielone zasoby inicjujesz,kk zrobie 
        TypyDniaController controller = new TypyDniaController();

        [Test]
        public void TestGetTable()
        {
            //porownoj sobie dane jakie otrzymasz z jakimis randomowymi jakie sam wymyslisz albo poprawnymi,
            //powinny byc poprawne i zahardkodowane np. w jakims pliku jsonowym/xmlowym.
        }

        [Test]
        [TestCase("11/7/2016")]
        public void TestGetday(string strData)
        {
            var currentDirectory = Environment.CurrentDirectory;
            currentDirectory = currentDirectory.Remove(currentDirectory.Length - 10);
            var fileDirectory = Path.Combine(currentDirectory, "TestData\\response.txt");
            var expectedLines = File.ReadAllLines(fileDirectory);
            var expected = string.Join("", expectedLines);
            dynamic expectedJson = JObject.Parse(expected);
            var expectedPosts = (JArray)expectedJson["Posts"];
            int counterEventDate = 0;
            int counterOdds = 0;
            int counterBranch = 0;
            int counterAnalyse = 0;

            var actual = controller.GetDay(strData);
            dynamic actualJson = JObject.Parse(actual);
            var actualPosts = (JArray)actualJson["Posts"];
            var minLength = Math.Min(expectedPosts.Count, actualPosts.Count);

            for (int i=0; i<minLength; i++)
            {
                var expectedEventDate = expectedPosts[i]["Prediction"]["EventDate"].ToString();
                var actualEventDate = actualPosts[i]["Prediction"]["EventDate"].ToString();

                var expectedOdds = expectedPosts[i]["Prediction"]["Odds"].ToString();
                var actualOdds = actualPosts[i]["Prediction"]["Odds"].ToString();

                var expectedBranch = expectedPosts[i]["Prediction"]["Branch"].ToString();
                var actualBranch = actualPosts[i]["Prediction"]["Branch"].ToString();

                var expectedAnalyse = expectedPosts[i]["Prediction"]["Analyse"].ToString();
                var actualAnalyse = actualPosts[i]["Prediction"]["Analyse"].ToString();

                if (expectedEventDate != actualEventDate)
                {
                    counterEventDate++;
                }

                if (expectedOdds != actualOdds)
                {
                    counterOdds++;
                }

                if (expectedBranch != actualBranch)
                {
                    counterBranch++;
                }

                if (expectedAnalyse != actualAnalyse)
                {
                    counterAnalyse++;
                }
            }

            Assert.AreEqual(counterEventDate, 0);
            Assert.AreEqual(counterOdds, 0);
            Assert.AreEqual(counterBranch, 0);
            Assert.AreEqual(counterAnalyse, 0);
        }

        [Test]
        public void TestGetWinnersArchives()
        {
            //to że metoda w kontrolerze nie jest napisana to nie oznacza 
            //że ty nie możesz napisać testów, po prostu będą failowane

            //ja nie wiem jak będzie tabela wyglądać, https://www.forum.bukmacherskie.com/f43/archiwum-zwyciezcow-typow-dnia-122961.html
            //ok wlasnie ja zrobilem

            var result = controller.GetWinnersArchives();
            dynamic data = JObject.Parse(result);
            var monthlyResults = data.MonthlyResults;

            foreach (var monthData in monthlyResults)
            {
                Assert.IsNotNull(monthData.MonthResult.Winners);
            }
        }
    }
}
