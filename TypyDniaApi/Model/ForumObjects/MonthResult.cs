using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TypyDniaApi.Model.ForumObjects
{
    public class MonthResult
    {
        public DateTime Date { get; set; }

        public Dictionary<int, string> Winners { get; set; } 
    }
}