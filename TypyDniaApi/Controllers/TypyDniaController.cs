using System;
using System.Linq;
using System.Web.Http;
using System.Web.Script.Serialization;
using HtmlAgilityPack;
using Newtonsoft.Json;
using TypyDniaApi.Model.DataSource.Contexts;
using TypyDniaApi.Model.Helpers;
using TypyDniaApi.Model.ForumObjects;
using TypyDniaApi.Model.Repostiories;

namespace TypyDniaApi.Controllers
{
    public class TypyDniaController : ApiController
    {
        private readonly ITableRepository _tableRepository;

        private readonly IDayRepository _dayRepository;

        public TypyDniaController()
        {
            IContext context = new TypyDniaContext();
            _dayRepository = new DayRepository(context);
            _tableRepository = new TableRepository(context);
        }

        public string GetWinnersArchives()
        {
            // https://www.forum.bukmacherskie.com/f43/archiwum-zwyciezcow-typow-dnia-122961.html

            //return all winners from the beggining [json]
            //po testach napiszesz strukture (serwis i repozytorium) do tej metody (na podstawie moich rozwiązań)

            return new JavaScriptSerializer().Serialize(new WinnersArchive());
        }

        [HttpPost]
        public string GetTable([FromBody] string date)
        {
            var requestedDate = DateTime.Parse(date);

            Table table = _tableRepository.GetTable(requestedDate);

            return JsonConvert.SerializeObject(table);
        }

        [HttpPost]
        public string GetDay([FromBody] string date)
        {
            var requestedDate = DateTime.Parse(date);

            Day day = _dayRepository.GetDay(requestedDate);

            //start test here:

            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load("https://www.whoscored.com/Teams/256");
            HtmlNodeCollection wrapper = document.DocumentNode.SelectNodes("//span[@class='team-header-name']");
            HtmlNodeCollection wrapper2 = document.DocumentNode.SelectNodes("//span[@id]");
            int f = 10;

            //end test here

            return JsonConvert.SerializeObject(day);
        }
    }
}
