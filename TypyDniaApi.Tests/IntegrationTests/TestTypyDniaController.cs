using System.Globalization;
using System.IO;
using System.Threading;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Shared;
using Shared.Model;
using TypyDniaApi.Controllers;
using TypyDniaApi.Model.DataSource.Contexts;
using TypyDniaApi.Model.Repostiories;
using Assert = NUnit.Framework.Assert;


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


        [TestMethod]
        public void TestGetTable()
        {
            //porownoj sobie dane jakie otrzymasz z jakimis randomowymi jakie sam wymyslisz albo poprawnymi,
            //powinny byc poprawne i zahardkodowane np. w jakims pliku jsonowym/xmlowym.
        }

        [TestMethod]
        public void TestGetday()
        {
            string data = "11/7/2016";
            //string expectedGetDayPath = Path.Combine(LocalPaths.TestData, "TypyDniaData", "response.txt");
            //string expectedGetDay = File.ReadAllText(expectedGetDayPath);
            string expectedGetDay = EmbeddedData.AsString("response.txt");
            dynamic expectedJson = JObject.Parse(expectedGetDay);
            var expectedPosts = (JArray)expectedJson["Posts"];

            string returnedDay = _controller.GetDay(data);
            dynamic actualJson = JObject.Parse(returnedDay);
            var actualPosts = (JArray)actualJson["Posts"];

            Assert.AreEqual(expectedPosts, actualPosts);
        }

        [TestMethod]
        public void TestGetWinnersArchives()
        {
            //to że metoda w kontrolerze nie jest napisana to nie oznacza 
            //że ty nie możesz napisać testów, po prostu będą failowane

            //ja nie wiem jak będzie tabela wyglądać, https://www.forum.bukmacherskie.com/f43/archiwum-zwyciezcow-typow-dnia-122961.html
            //ok wlasnie ja zrobilem

            string expectedArchivePath = "";
            string expectedWinnersArchive = File.ReadAllText(expectedArchivePath);

            string actualWinnersArchive = _controller.GetWinnersArchives();

            Assert.AreEqual(expectedWinnersArchive, actualWinnersArchive);
        }
    }
}
