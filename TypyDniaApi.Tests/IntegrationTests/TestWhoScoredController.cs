using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Shared.Model.Requests;
using TypyDniaApi.Controllers;
using TypyDniaApi.Model.Services;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using Shared;
using TypyDniaApi.Model.DataSource;
using TypyDniaApi.Model.Repostiories;
using EmbeddedData = TypyDniaApi.Tests.TestData.EmbeddedData;

namespace TypyDniaApi.Tests.IntegrationTests
{
    [TestClass]
    public class TestWhoScoredController
    {
        private WhoScoredController _controller;

        [SetUp]
        public void Setup()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
            _controller = new WhoScoredController(new WhoScoredService(new MatchDetailsRepository(new WhoScoredScraper())));
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void TestGetMatchDetails()
        {
            string expectedMatchDetails = EmbeddedData.AsString("matchDetails.txt");
            var expectedJson = JObject.Parse(expectedMatchDetails);

            var testMatchRequest = new MatchRequest
            {
                HomeTeamId = 75,
                Date = "30-10-16"
            };

            string actualMatchDetails = _controller.GetMatchDetails(testMatchRequest);
            var actualJson = JObject.Parse(actualMatchDetails);

            Assert.AreEqual(expectedJson.ToString(), actualJson.ToString());
        }

        [Test]
        public void TestGetSeasonMatches()
        {
            string expectedSeasonRequests = EmbeddedData.AsString("seasonMatches-seriea-2016-2017.txt");
            var expectedJson = JArray.Parse(expectedSeasonRequests);

            var testSeasonRequest = new SeasonRequest
            {
                League = "Serie A",
                Years = "2016/2017"
            };

            string actualSeasonRequests = _controller.GetSeasonMatches(testSeasonRequest);
            var actualJson = JArray.Parse(actualSeasonRequests);

            Assert.AreEqual(expectedJson.ToString(), actualJson.ToString());
        }
    }
}
