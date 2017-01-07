using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using TypyDniaApi.Controllers;
using TypyDniaApi.Model.DataSource.Contexts;
using TypyDniaApi.Model.Repostiories;
using Assert = NUnit.Framework.Assert;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypyDniaApi.Tests.TestData;

namespace TypyDniaApi.Tests.IntegrationTests
{
    [TestClass]
    public class TestTypyDniaController
    {
        private TypyDniaController _controller;

        [SetUp]
        public void Setup()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
            _controller = new TypyDniaController(new DayRepository(new TypyDniaContext()), new TableRepository(new TypyDniaContext()));
        }

        [TearDown]
        public void TearDown()
        {
            _controller = null;
        }


        [Test]
        public void TestGetTable()
        {
            //todo
        }

        [Test]
        public void TestGetday()
        {
            string data = "11/7/2016";
            string expectedGetDay = EmbeddedData.AsString("response.txt");
            dynamic expectedJson = JObject.Parse(expectedGetDay);
            var expectedPosts = (JArray)expectedJson["Posts"];

            string returnedDay = _controller.GetDay(data);
            dynamic actualJson = JObject.Parse(returnedDay);
            var actualPosts = (JArray)actualJson["Posts"];

            Assert.AreEqual(expectedPosts.Children().Count(), actualPosts.Children().Count());

            var rnd = new Random();
            int postNum = rnd.Next(0, actualPosts.Children().Count());
            var expectedPrediction = expectedPosts[postNum]["Prediction"];
            var actualPrediction = actualPosts[postNum]["Prediction"];

            Assert.AreEqual(expectedPrediction, actualPrediction);
        }

        [Test]
        public void TestGetWinnersArchives()
        {
            string expectedArchive = EmbeddedData.AsString("archive.txt");
            dynamic expectedJson = JObject.Parse(expectedArchive);

            string actualArchive = _controller.GetWinnersArchives();
            dynamic actualJson = JObject.Parse(actualArchive);

            Assert.AreEqual(expectedJson.Data, actualJson.Data);
        }
    }
}
