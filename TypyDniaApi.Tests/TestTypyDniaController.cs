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

        private Mock<IDayRepository> mockDayRepository;
        private Mock<ITableRepository> mockTableRepository;
        private TypyDniaController controller;
        private string expectedGetDay;
        private string expectedGetDayPath;
        private dynamic expectedJson;
        private int singlePostIndex;

        private string expectedWinnersArchive;
        private string expectedArchivesPath;

        [SetUp]
        public void Setup()
        {
            mockDayRepository = new Mock<IDayRepository>();
            mockTableRepository = new Mock<ITableRepository>();
            controller = new TypyDniaController(mockDayRepository.Object, mockTableRepository.Object);
            
            expectedGetDayPath = string.Empty;
            expectedGetDay = File.ReadAllText(expectedGetDayPath);

            expectedJson = JObject.Parse(expectedGetDay);
            singlePostIndex = 1;

            expectedWinnersArchive = File.ReadAllText(expectedArchivesPath);
        }

        [TearDown]
        public void TearDown()
        {
            mockDayRepository = null;
            mockTableRepository = null;
            controller = null;
            expectedGetDay = string.Empty;
            expectedGetDayPath = string.Empty;
            expectedJson = null;
            expectedWinnersArchive = string.Empty;
            expectedArchivesPath = string.Empty;
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
            //expectedGetDay = File.ReadAllText(expectedGetDayPath);
            //dynamic expectedJson = JObject.Parse(expectedGetDay);
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

            dynamic expectedArchiveJson = JObject.Parse(expectedWinnersArchive);

            string actualArchiveJson = controller.GetWinnersArchives();
        }
    }
}
