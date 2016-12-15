using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using TypyDniaApi.Controllers;
using TypyDniaApi.Model.Requests;
using TypyDniaApi.Model.Services;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TypyDniaApi.Tests
{
    [TestClass]
    public class TestWhoScoredController
    {
        private WhoScoredController controller;
        private Mock<IWhoScoredService> mockService;
        private MatchRequest testMatchRequest;
        private SeasonRequest testSeasonRequest;
        private string expectedMatchDetails;
        private string expectedDetailsPath;
        private string expectedSeasonRequests;
        private string expectedSeasonPath;

        [SetUp]
        public void Setup()
        {
            mockService = new Mock<IWhoScoredService>();
            controller = new WhoScoredController(mockService.Object);

            testMatchRequest = new MatchRequest();
            testMatchRequest.HomeTeamId = 75;
            testMatchRequest.Date = "30-10-16";

            testSeasonRequest = new SeasonRequest();
            testSeasonRequest.League = "Serie A";
            testSeasonRequest.Years = "2014/2015";

            expectedMatchDetails = string.Empty;
            expectedDetailsPath = string.Empty;

            expectedSeasonRequests = string.Empty;
            expectedSeasonPath = string.Empty;
        }

        [TearDown]
        public void TearDown()
        {
            controller = null;
            mockService = null;

            testMatchRequest = null;

            testSeasonRequest = null;

            expectedMatchDetails = string.Empty;
            expectedDetailsPath = string.Empty;

            expectedSeasonRequests = string.Empty;
            expectedSeasonPath = string.Empty;
        }

        [Test]
        public void TestOne()
        {
            // Do something...
        }

        [Test]
        public void TestGetMatchDetails()
        {
            expectedMatchDetails = File.ReadAllText(expectedDetailsPath);

            string actualMatchDetails = controller.GetMatchDetails(testMatchRequest);

            Assert.AreEqual(expectedMatchDetails, actualMatchDetails);
        }

        [Test]
        public void TestGetSeasonMatches()
        {
            expectedSeasonRequests = File.ReadAllText(expectedSeasonPath);

            string actualSeasonRequests = controller.GetSeasonMatches(testSeasonRequest);

            Assert.AreEqual(expectedSeasonRequests, actualSeasonRequests);
        }
    }
}
