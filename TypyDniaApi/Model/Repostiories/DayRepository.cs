using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using TypyDniaApi.Model.DataSource.Contexts;
using TypyDniaApi.Model.Helpers;
using TypyDniaApi.Model.ForumObjects;

namespace TypyDniaApi.Model.Repostiories
{
    public class DayRepository : IDayRepository
    {
        private readonly IContext _context;

        public DayRepository(IContext ctx)
        {
            _context = ctx;
        }

        public Day GetDay(DateTime date)
        {
            Day day = _context.GetDay(date);
            
            return day;
        }

        public TableThread GetTableThread(DateTime date)
        {

            return new TableThread(date);
        }
    }
}