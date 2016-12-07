using System;
using System.Collections.Generic;

namespace TypyDniaApi.Model.ForumObjects
{
    public class TableThread
    {
        public byte[] Scoreboard { get; set; }

        public List<Post> Posts { get; set; }

        public DateTime ThreadDate { get { return _date; } }

        private DateTime _date;

        public TableThread(DateTime date)
        {
            _date = date;
        }
    }
}