using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
            _controller = new WhoScoredController(new WhoScoredService(new MatchDetailsRepository(new WhoScoredScraper())));
        }

        [TearDown]
        public void TearDown()
        {

        }

        [TestMethod]
        public void TestGetMatchDetails()
        {
            string expectedMatchDetails = EmbeddedData.AsString("matchDetails.txt");

            var testMatchRequest = new MatchRequest();
            testMatchRequest.HomeTeamId = 75;
            testMatchRequest.Date = "30-10-16";

            string actualMatchDetails = _controller.GetMatchDetails(testMatchRequest);

            Assert.AreEqual(expectedMatchDetails, actualMatchDetails);
        }

        [TestMethod]
        public void TestGetSeasonMatches()
        {
            string expectedSeasonRequests = EmbeddedData.AsString("seasonMatches-seriea-2014-2015.txt");

            var testSeasonRequest = new SeasonRequest();
            testSeasonRequest.League = "Serie A";
            testSeasonRequest.Years = "2014/2015";

            string actualSeasonRequests = _controller.GetSeasonMatches(testSeasonRequest);

            Assert.AreEqual(expectedSeasonRequests, actualSeasonRequests);
        }
    }
}
