using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TypyDniaApi.Model.DataSource.Contexts;
using TypyDniaApi.Model.ForumObjects;

namespace TypyDniaApi.Model.Repostiories
{
    public class TableRepository : ITableRepository
    {
        private readonly IContext _context;

        public TableRepository(IContext context)
        {
            _context = context;
        }

        public Table GetTable(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}