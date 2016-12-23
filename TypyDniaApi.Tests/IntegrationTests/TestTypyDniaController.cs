using System.Globalization;
using System.IO;
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
            //porownoj sobie dane jakie otrzymasz z jakimis randomowymi jakie sam wymyslisz albo poprawnymi,
            //powinny byc poprawne i zahardkodowane np. w jakims pliku jsonowym/xmlowym.
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

            Assert.AreEqual(expectedPosts, actualPosts);
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
