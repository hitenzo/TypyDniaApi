using System.IO;
using Moq;
using Newtonsoft.Json.Linq;
using TypyDniaApi.Controllers;
using NUnit.Framework;
using TypyDniaApi.Model.Repostiories;
using Assert = NUnit.Framework.Assert;


namespace TypyDniaApi.Tests
{
    [TestFixture]
    public class TestTypyDniaController
    {
        //w stupie wszystkie wspodzielone zasoby inicjujesz,kk zrobie 

        private TypyDniaController controller;

        [SetUp]
        public void Setup()
        {
            var mockDayRepository = new Mock<IDayRepository>();
            var mockTableRepository = new Mock<ITableRepository>();
            controller = new TypyDniaController(mockDayRepository.Object, mockTableRepository.Object);
        }

        [TearDown]
        public void TearDown()
        {
            controller = null;
        }


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
            string expectedGetDayPath = "";
            string expectedGetDay = File.ReadAllText(expectedGetDayPath);
            dynamic expectedJson = JObject.Parse(expectedGetDay);
            var expectedPosts = (JArray)expectedJson["Posts"];

            var actual = controller.GetDay(strData);
            dynamic actualJson = JObject.Parse(actual);
            var actualPosts = (JArray)actualJson["Posts"];

            Assert.AreEqual(expectedPosts, actualPosts);
        }

        [Test]
        public void TestGetWinnersArchives()
        {
            //to że metoda w kontrolerze nie jest napisana to nie oznacza 
            //że ty nie możesz napisać testów, po prostu będą failowane

            //ja nie wiem jak będzie tabela wyglądać, https://www.forum.bukmacherskie.com/f43/archiwum-zwyciezcow-typow-dnia-122961.html
            //ok wlasnie ja zrobilem

            string expectedArchivePath = "";
            string expectedWinnersArchive = File.ReadAllText(expectedArchivePath);

            string actualWinnersArchive = controller.GetWinnersArchives();

            Assert.AreEqual(expectedWinnersArchive, actualWinnersArchive);
        }
    }
}
